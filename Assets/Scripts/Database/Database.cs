using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using static Enums;

public class Database
{
    private const string DatabaseName = "RunesOfDestiny.s3db";
    private const string UserTable = "users";
    private const string NpcTable = "npc";
    private const string QuestTable = "quest";
    private const string ItemsTable = "items";

    private string _connPath;
    private IDbConnection _dbConn;

    public static bool Initialized = false;

    public Database()
    {
        _connPath = $"URI=file:{Application.dataPath}/{DatabaseName}";
        _dbConn = new SqliteConnection(_connPath);

        InitializeDatabase();
    }

    public void InitializeDatabase()
    {
        if (Initialized) return;
        DropTable_Users();
        DropTable_Items();
        DropTable_Npc();
        DropTable_Quest();

        CreateTable_Users();
        CreateTable_Items();
        CreateTable_Npc();
        CreateTable_Quest();
        CreateItems();
        CreatePlayer();
        CreateQuests();
        CreateNpcs();
        Debug.Log("Database Initialized");
        Initialized = true;
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

    private void DropTable_Npc()
    {
        try
        {
            string query = $"DROP TABLE IF EXISTS {NpcTable}";
            PostQueryToDb(query);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error dropping table {NpcTable}: {e}");
        }
    }

    private void DropTable_Quest()
    {
        try
        {
            string query = $"DROP TABLE IF EXISTS {QuestTable}";
            PostQueryToDb(query);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error dropping table {QuestTable}: {e}");
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
                            "item_rarity INTEGER NOT NULL," +
                            "drop_chance DOUBLE PRECISION NOT NULL," +
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

    private void CreateTable_Npc()
    {
        string query = $"CREATE TABLE IF NOT EXISTS {NpcTable}(" +
                            "npc_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "name VARCHAR(200)," +
                            "quest_ids TEXT" +
                            ")";
        PostQueryToDb(query);
    }

    private void CreateTable_Quest()
    {
        string query = $"CREATE TABLE IF NOT EXISTS {QuestTable}(" +
                            "quest_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "title VARCHAR(200) NOT NULL," +
                            "description TEXT NOT NULL," +
                            "objective VARCHAR(255) NOT NULL," +
                            "item_rewards TEXT," +
                            "kill_count INTEGER " +
                            ")";
        PostQueryToDb(query);
    }

    private void CreateItems()
    {
        string queryBodyArmor1 = $"INSERT INTO {ItemsTable} (name, sprite, item_type, item_rarity, drop_chance, max_health) VALUES ('Mail Armor', 'ArmorAndJewelry/Icons/BodyArmor/BodyArmor_1', '{(int)ItemType.Armor}', '{(int)Rarity.Common}', 0.5, 500)";
        string queryBodyArmor3 = $"INSERT INTO {ItemsTable} (name, sprite, item_type, item_rarity, drop_chance, max_mana) VALUES ('Mage Armor', 'ArmorAndJewelry/Icons/BodyArmor/BodyArmor_3', '{(int)ItemType.Armor}', '{(int)Rarity.Uncommon}', 0.25, 300)";
        PostQueryToDb(queryBodyArmor1);
        PostQueryToDb(queryBodyArmor3);


        string queryBoots6 = $"INSERT INTO {ItemsTable} (name, sprite, item_type, item_rarity, drop_chance, movement_speed) VALUES ('Light Boots', 'ArmorAndJewelry/Icons/Boots/Boots_6', '{(int)ItemType.Boots}', '{(int)Rarity.Rare}', 0.20, 5)";
        string queryBoots8 = $"INSERT INTO {ItemsTable} (name, sprite, item_type, item_rarity, drop_chance, movement_speed, armor) VALUES ('Heavy Boots', 'ArmorAndJewelry/Icons/Boots/Boots_8', '{(int)ItemType.Boots}', '{(int)Rarity.Rare}', 0.2, -5, 500)";
        PostQueryToDb(queryBoots6);
        PostQueryToDb(queryBoots8);


        string queryAdminBoots = $"INSERT INTO {ItemsTable} (name, sprite, item_type, item_rarity, drop_chance, movement_speed) VALUES ('Admin Boots', 'ArmorAndJewelry/Icons/Boots/Boots_5', '{(int)ItemType.Boots}', '{(int)Rarity.Mythic}', 0.0, 50)";
        string queryAdminRing = $"INSERT INTO {ItemsTable} (name, sprite, item_type, item_rarity, drop_chance, max_health, max_mana, evasion_chance, health_regen, mana_regen) VALUES ('Admin Ring', 'ArmorAndJewelry/Icons/Rings/Ring_1', '{(int)ItemType.Rings}', '{(int)Rarity.Mythic}', 0.0, 100000, 100000, 100, 1000, 1000)";
        string queryIkfirus = $"INSERT INTO {ItemsTable} (name, sprite, item_type, item_rarity, drop_chance, water_resistance, fire_resistance, lightning_resistance, void_resistance) VALUES ('Ikfirus Armor', 'ArmorAndJewelry/Icons/BodyArmor/BodyArmor_8', '{(int)ItemType.Armor}', '{(int)Rarity.Legendary}', 1.0, 80, 80, 80, 80)";
        PostQueryToDb(queryIkfirus);
        PostQueryToDb(queryAdminBoots);
        PostQueryToDb(queryAdminRing);
    }

    private void CreatePlayer()
    {
        int[] inventory = new int[] { 1, 2, 3, 4, 5, 6, 7 };
        string inventoryString = string.Join(",", inventory);
        inventoryString = inventoryString.Replace("'", "''");
        string query = $"INSERT INTO {UserTable} (name, inventory) VALUES ('player', '{inventoryString}')";
        PostQueryToDb(query);
    }

    private void CreateNpcs()
    {
        int[] questId = new int[] { 1, 2, 3 };
        string questString = string.Join(",", questId).Replace("'", "''");
        string query = $"INSERT INTO {NpcTable} (name, quest_ids) VALUES ('starter', '{questString}')";
        Debug.Log("NPC Created");
        PostQueryToDb(query);
    }

    private void CreateQuests()
    {
        int[] reward = new int[] { 1 };
        string rewardString = string.Join(",", reward);
        rewardString = rewardString.Replace("'", "''");
        string gettingStartedQuery = $"INSERT INTO {QuestTable} (title, description, objective) VALUES ('Getting Started', 'Ah, greetings! I have been eagerly awaiting your arrival. Welcome to Eldoria, the heart of our mystical world. My name is Elysia, and I am here to guide you on your extraordinary journey throughout the realms of magic and destiny. \n\nAs your first task, lets begin by exploring the outskirts of Eldoria and familiarizing yourself with the basics, equip an item in your inventory.', '- Equip an item\n')";
        string eliminateTheMenaceQuery = $"INSERT INTO {QuestTable} (title, description, objective, kill_count) VALUES ('Eliminate The Menace', 'Greetings! Eldoria faces a growing threat from hostile forces, and your assistance is crucial. We have identified a group of mischievous pirates in the vicinity that need to be dealt with. Your task is to eliminate 5 of these enemies. Head east from here, vanquish the foes, and ensure the safety of Eldoria. May your blade be swift and true!\r\n', '- Kill 5 enemies\n', 5)";
        string wantedCaptainQuery = $"INSERT INTO {QuestTable} (title, description, objective, item_rewards) VALUES ('WANTED: Captain Darkblade', 'Ahoy! There is a notorious figure causing chaos in Eldoria, and we need your skills to put an end to their mischief. We have received reports of a formidable enemy known as Captain Darkblade. This rogue captain is Wanted for various crimes against the realm. Your mission, should you choose to accept it, is to track down and defeat Captain Darkblade. Exercise caution, for the captain is rumored to be a fierce adversary.\n\nEldorias fate rests in your hands!\r\n', '- Kill Captain Darkblade\n', '{rewardString}')";
        PostQueryToDb(gettingStartedQuery);
        PostQueryToDb(eliminateTheMenaceQuery);
        PostQueryToDb(wantedCaptainQuery);
    }

    public int[] GetQuestsFromNpc(int npcId)
    {
        try
        {
            if (_dbConn.State != ConnectionState.Open)
            {
                _dbConn.Open();
            }

            IDbCommand cmd = _dbConn.CreateCommand();
            string query = $"SELECT npc_id, quest_ids FROM {NpcTable} WHERE npc_id = {npcId}";
            int[] questsIds = null;
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string questIds = reader.GetString(1);
                Debug.Log($"QuestIds {questIds}");
                string[] questArrayString = questIds.Split(',');

                questsIds = Array.ConvertAll(questArrayString, int.Parse);
            }

            cmd.Dispose();
            _dbConn.Close();

            return questsIds;

        }
        catch (System.Exception e)
        {
            Debug.LogError($"POST QUERY ERROR: {e}");
            _dbConn.Close();
            return null;
        }
    }

    public QuestData GetQuest(int questId)
    {
        try
        {
            if (_dbConn.State != ConnectionState.Open)
            {
                _dbConn.Open();
            }
            IDbCommand cmd = _dbConn.CreateCommand();
            string query = $"SELECT quest_id, title, description, objective, COALESCE(item_rewards, ''), COALESCE(kill_count, 0) FROM {QuestTable} WHERE quest_id = {questId}";
            QuestData questData = null;
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                string description = reader.GetString(2);
                string objective = reader.GetString(3);
                string rewardString = reader.GetString(4);
                int killCount = reader.GetInt32(5);
                string[] rewardArrayString = rewardString.Split(',');
                int[] rewards = rewardString.Length > 0 ? Array.ConvertAll(rewardArrayString, int.Parse) : new int[0];

                questData = new QuestData(id, title, description, objective, rewards, killCount);

            }

            cmd.Dispose();
            _dbConn.Close();

            return questData;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"POST QUERY ERROR: {e}");
            _dbConn.Close();
            return null;
        }
    }

    public QuestData[] GetQuests(int npcId)
    {
        try
        {
            int[] questsIds = GetQuestsFromNpc(npcId);
            if (_dbConn.State != ConnectionState.Open)
            {
                _dbConn.Open();
            }
            if (questsIds.Length <= 0) return null;
            string questIdList = string.Join(",", questsIds);

            IDbCommand cmd = _dbConn.CreateCommand();
            string query = $"SELECT quest_id, title, description, objective, COALESCE(item_rewards, '') FROM {QuestTable} WHERE quest_id IN ({questIdList})";
            List<QuestData> questsData = new();
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                QuestData questData = null;
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                string description = reader.GetString(2);
                string objective = reader.GetString(3);
                string rewardString = reader.GetString(4);
                string[] rewardArrayString = rewardString.Split(',');
                int[] rewards = rewardString.Length > 0 ? Array.ConvertAll(rewardArrayString, int.Parse) : new int[0];

                questData = new QuestData(id, title, description, objective, rewards);
                questsData.Add(questData);

            }

            cmd.Dispose();
            _dbConn.Close();

            return questsData.ToArray();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"POST QUERY ERROR: {e}");
            _dbConn.Close();
            return null;
        }
    }

    public ItemData GetItem(int itemId)
    {
        try
        {
            if (_dbConn.State != ConnectionState.Open)
            {
                _dbConn.Open();
            }

            IDbCommand cmd = _dbConn.CreateCommand();
            string query = $"SELECT item_id, name, sprite, item_type, item_rarity, drop_chance, COALESCE(max_health, 0), COALESCE(max_mana, 0), COALESCE(movement_speed, 0), COALESCE(armor, 0), COALESCE(evasion_chance, 0), COALESCE(water_resistance, 0), COALESCE(lightning_resistance, 0), COALESCE(fire_resistance, 0), COALESCE(void_resistance, 0), COALESCE(health_regen, 0), COALESCE(mana_regen, 0) FROM {ItemsTable} WHERE item_id = {itemId}";
            ItemData itemData = null;
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string spritePath = reader.GetString(2);
                ItemType itemType = (ItemType)reader.GetInt32(3);
                Rarity rarity = (Rarity)reader.GetInt32(4);
                double dropChance = reader.GetDouble(5);
                int maxHealth = reader.GetInt32(6);
                int maxMana = reader.GetInt32(7);
                int movementSpeed = reader.GetInt32(8);
                int armor = reader.GetInt32(9);
                int evasionChance = reader.GetInt32(10);
                int waterResistance = reader.GetInt32(11);
                int lightningResistance = reader.GetInt32(12);
                int fireResistance = reader.GetInt32(13);
                int voidResistance = reader.GetInt32(14);
                int healthRegen = reader.GetInt32(15);
                int manaRegen = reader.GetInt32(16);

                EntityStatsValues statsValues = new()
                {
                    MaxLife = maxHealth
                };

                CharacterStatsValues characterStatsValues = new()
                {
                    MaxMana = maxMana,
                    MovementSpeed = movementSpeed,
                    HealthRegen = healthRegen,
                    ManaRegen = manaRegen
                };

                CharacterDefensiveStatsValues characterDefensiveStatsValues = new()
                {
                    Armor = armor,
                    EvasionChance = evasionChance,
                    WaterResistance = waterResistance,
                    LightningResistance = lightningResistance,
                    FireResistance = fireResistance,
                    VoidResistance = voidResistance
                };
                CharacterStats characterStats = ScriptableObject.CreateInstance<CharacterStats>();
                characterStats.SetEntityStats(statsValues);
                characterStats.SetCharacterStatsValues(characterStatsValues);
                characterStats.SetCharacterDefensiveStatsValues(characterDefensiveStatsValues);
                

                Sprite sprite = Resources.Load<Sprite>(spritePath);
                Debug.Log($"Sprite is {sprite} with path {spritePath}");
                itemData = new ItemData(id, name, characterStats, sprite, itemType, rarity, dropChance);

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
            if (_dbConn.State != ConnectionState.Open)
            {
                _dbConn.Open();
            }

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
