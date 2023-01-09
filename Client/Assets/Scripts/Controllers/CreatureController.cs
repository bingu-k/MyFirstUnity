using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureController : MonoBehaviour
{
    public float _speed = 5.0f;
    public Vector3Int CellPos { get; set; } = Vector3Int.zero;

    protected SpriteRenderer _sprite;
    protected Animator _anim;

    protected CreatureState _state = CreatureState.Idle;
    public virtual CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;
            _state = value;
            UpdateAnimation();
        }
    }

    protected MoveDir _lastDir = MoveDir.Down;
    protected MoveDir _dir = MoveDir.None;

    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;
            _dir = value;
            if (value != MoveDir.None)
                _lastDir = value;
            UpdateAnimation();
        }
    }
    
    public MoveDir GetDirFromVec(Vector3Int dir)
    {
        if (dir.x > 0)
            return MoveDir.Right;
        else if (dir.x < 0)
            return MoveDir.Left;
        else if (dir.y > 0)
            return MoveDir.Up;
        else if (dir.y < 0)
            return MoveDir.Down;
        else
            return MoveDir.None;
    }

    public Vector3Int GetFrontCellPos()
    {
        Vector3Int cellpos = CellPos;
        switch (_lastDir)
        {
            case MoveDir.Up:
                cellpos += Vector3Int.up;
                break;
            case MoveDir.Down:
                cellpos += Vector3Int.down;
                break;
            case MoveDir.Left:
                cellpos += Vector3Int.left;
                break;
            case MoveDir.Right:
                cellpos += Vector3Int.right;
                break;
        }
        return cellpos;
    }

    protected virtual void UpdateAnimation()
    {
        if (_state == CreatureState.Idle)
        {
            switch (_lastDir)
            {
                case MoveDir.Up:
                    _anim.Play("IDLE_BACK");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _anim.Play("IDLE_FRONT");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _anim.Play("IDLE_RIGHT");
                    _sprite.flipX = true;
                    break;
                case MoveDir.Right:
                    _anim.Play("IDLE_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else if (_state == CreatureState.Moving)
        {
            switch (_dir)
            {
                case MoveDir.Up:
                    _anim.Play("WALK_BACK");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _anim.Play("WALK_FRONT");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _anim.Play("WALK_RIGHT");
                    _sprite.flipX = true;
                    break;
                case MoveDir.Right:
                    _anim.Play("WALK_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else if (_state == CreatureState.Skill)
        {
            switch (_lastDir)
            {
                case MoveDir.Up:
                    _anim.Play("ATTACK_BACK");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _anim.Play("ATTACK_FRONT");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _anim.Play("ATTACK_RIGHT");
                    _sprite.flipX = true;
                    break;
                case MoveDir.Right:
                    _anim.Play("ATTACK_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else
        {

        }
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        UpdateController();
    }

    protected virtual void Init()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        transform.position = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
    }

    protected virtual void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Idle:
                UpdateIdle();
                break;
            case CreatureState.Moving:
                UpdateMoving();
                break;
            case CreatureState.Skill:
                UpdateSkill();
                break;
            case CreatureState.Dead:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle()
    {}

    protected virtual void UpdateMoving()
    {
        if (State != CreatureState.Moving)
            return;

        Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;

        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            MoveToNextPos();
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = CreatureState.Moving;
        }
    }

    protected virtual void UpdateSkill()
    { }

    protected virtual void UpdateDead()
    { }

    public virtual void OnDamaged()
    { }

    protected virtual void MoveToNextPos()
    {
        if (_dir == MoveDir.None)
        {
            State = CreatureState.Idle;
            return;
        }

        Vector3Int destPos = CellPos;
        switch (Dir)
        {
            case MoveDir.Up:
                destPos += Vector3Int.up;
                break;
            case MoveDir.Down:
                destPos += Vector3Int.down;
                break;
            case MoveDir.Left:
                destPos += Vector3Int.left;
                break;
            case MoveDir.Right:
                destPos += Vector3Int.right;
                break;
        }
        State = CreatureState.Moving;
        if (Managers.Map.CanGo(destPos))
        {
            if (Managers.Object.Find(destPos) == null)
                CellPos = destPos;
        }
    }
}
