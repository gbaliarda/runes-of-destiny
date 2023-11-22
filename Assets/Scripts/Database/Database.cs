using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEditor.Search;
using UnityEngine;
using static Enums;

public class Database
{
    private const string DatabaseName = "RunesOfDestiny.s3db";
    private const string UserTable = "users";
    private const string ItemsTable = "items";

    private string _connPath;
    private IDbConnection _dbConn;

    public Database()
    {
        _connPath = $"URI=file:{Application.dataPath}/{DatabaseName}";
        _dbConn = new SqliteConnection(_connPath);

    }

    public void InitializeDatabase()
    {
        DropTable_Users();
        DropTable_Items();

        CreateTable_Users();
        CreateTable_Items();
        CreateItems();
        CreatePlayer();
        Debug.Log("Database Initialized");
    }

    private void PostQueryToDb(string query)
    {
        try
        {
            _dbConn.Open();

            IDbCommand cmd = _dbConn.CreateCommand();

            Debug.Log($"Query is: {query}");
            cmd.CommandText = query;
            cmd.ExecuteReader();

            cmd.Dispose();

        }
        catch (System.Exception e)
        {
            Debug.LogError($"POST QUERY ERROR: {e} with query {query}");

        }
        finally
        {
            _dbConn.Close();
        }
    }

    private void DropTable_Users()
    {
        try
        {
            string query = $"DROP TABLE IF EXISTS {UserTable}";
            PostQueryToDb(query);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error dropping table {UserTable}: {e}");
        }
    }

    private void DropTable_Items()
    {
        try
        {
            string query = $"DROP TABLE IF EXISTS {ItemsTable}";
            PostQueryToDb(query);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error dropping table {ItemsTable}: {e}");
        }
    }

    private void CreateTable_Users()
    {
        string query = $"CREATE TABLE IF NOT EXISTS {UserTable}(" +
                            "user_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "name VARCHAR(200) NOT NULL," +
                            "inventory TEXT" +
                            ")";
        PostQueryToDb(query);
    }

    private void CreateTable_Items()
    {
        string query = $"CREATE TABLE IF NOT EXISTS {ItemsTable}(" +
                            "item_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "name VARCHAR(200) NOT NULL," +
                            "sprite VARCHAR(255) NOT NULL," +
                            "item_type INTEGER NOT NULL," +
                            "max_health INTEGER," +
                            "max_mana INTEGER," +
                            "movement_speed INTEGER," +
                            "health_regen INTEGER," +
                            "mana_regen INTEGER," +
                            "dexterity INTEGER," +
                            "strength INTEGER," +
                            "intelligence INTEGER," +
                            "armor INTEGER," +
                            "evasion_chance INTEGER," +
                            "block_spell_chance INTEGER," +
                            "water_resistance INTEGER," +
                            "fire_resistance INTEGER," +
                            "lightning_resistance INTEGER," +
                            "void_resistance INTEGER," +
                            "damage_blocked_amount INTEGER" +
                            ")";
        PostQueryToDb(query);
    }

    private void CreateItems()
    {
        string queryBodyArmor1 = $"INSERT INTO {ItemsTable} (name, sprite, item_type, max_health) VALUES ('Mail Armor', 'ArmorAndJewelry/Icons/BodyArmor/BodyArmor_1', '{(int)ItemType.Armor}', 500)";
        string queryBodyArmor3 = $"INSERT INTO {ItemsTable} (name, sprite, item_type, max_mana) VALUES ('Mage Armor', 'ArmorAndJewelry/Icons/BodyArmor/BodyArmor_3', '{(int)ItemType.Armor}', 300)";
        PostQueryToDb(queryBodyArmor1);
        PostQueryToDb(queryBodyArmor3);
    }

    private void CreatePlayer()
    {
        int[] inventory = new int[] { 1, 2, 3 };
        string inventoryString = string.Join(",", inventory);
        inventoryString = inventoryString.Replace("'", "''");
        string query = $"INSERT INTO {UserTable} (name, inventory) VALUES ('player', '{inventoryString}')";
        PostQueryToDb(query);
    }

    public ItemData GetItem(int itemId)
    {
        try
        {
            _dbConn.Open();

            IDbCommand cmd = _dbConn.CreateCommand();
            string query = $"SELECT item_id, name, sprite, item_type, COALESCE(max_health, 0), COALESCE(max_mana, 0) FROM {ItemsTable} WHERE item_id = {itemId}";
            ItemData itemData = null;
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string spritePath = reader.GetString(2);
                ItemType itemType = (ItemType)reader.GetInt32(3);
                int maxHealth = reader.GetInt32(4);
                int maxMana = reader.GetInt32(5);

                EntityStatsValues statsValues = new EntityStatsValues();
                statsValues.MaxLife = maxHealth;
                CharacterStatsValues characterStatsValues = new CharacterStatsValues();
                characterStatsValues.MaxMana = maxMana;
                CharacterStats characterStats = ScriptableObject.CreateInstance<CharacterStats>();
                characterStats.SetEntityStats(statsValues);
                characterStats.SetCharacterStatsValues(characterStatsValues);
                

                Sprite sprite = Resources.Load<Sprite>(spritePath);
                Debug.Log($"Sprite is {sprite} with path {spritePath}");
                itemData = new ItemData(id, name, characterStats, sprite, itemType);

            }

            cmd.Dispose();
            _dbConn.Close();

            return itemData;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"POST QUERY ERROR: {e}");
            _dbConn.Close();
            return null;
        }
    }

    private void CreateTable_Inventory()
    {

    }

    public void AddUser(Player player)
    {
        string query = $"INSERT INTO {UserTable} (inventory) VALUES ('')";
        PostQueryToDb(query);
    }

    public UserData GetUser(int userId)
    {
        try
        {
            _dbConn.Open();

            IDbCommand cmd = _dbConn.CreateCommand();
            string query = $"SELECT user_id, inventory FROM {UserTable} WHERE user_id = {userId}";
            UserData userData = null;
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                int id = reader.GetInt32(0);
                string inventoryString = reader.GetString(1);
                string[] inventoryArrayString = inventoryString.Split(',');

                userData = new UserData(id, "player", Array.ConvertAll(inventoryArrayString, int.Parse));
            }

            cmd.Dispose();
            _dbConn.Close();

            return userData;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"POST QUERY ERROR: {e}");
            _dbConn.Close();
            return null;
        }
    }
}
