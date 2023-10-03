using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdPlayAnimation : ICommand
{
    private Animator _animator;
    private string _animation;
    private string _oldAnimation;

    public CmdPlayAnimation(Animator animator, string animation)
    {
        _animator = animator;
        _oldAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        _animation = animation;
    }

    public void Execute() => _animator.Play(_animation);

    public void Undo() => _animator.Play(_oldAnimation);
}
