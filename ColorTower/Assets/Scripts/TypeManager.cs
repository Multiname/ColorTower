using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeManager : MonoBehaviour
{
    public enum Type
    {
        Green,
        Yellow,
        Red,
        Blue,
        Lime,
        Brown,
        Purple,
        Azure,
        Black,
        White
    }

    public readonly List<Color> typeColors = new() {
        Color.green, Color.yellow, Color.red, Color.blue,
        new Color(0.68f, 0.84f, 0.03f), new Color(0.93f, 0.54f, 0.07f), new Color(0.8f, 0.12f, 0.69f), Color.cyan,
        new Color(0.2f, 0.2f, 0.2f), new Color(0.8f, 0.8f, 0.8f)};

    public Sprite[] enemySprites = new Sprite[10];
    public AnimatorOverrideController[] enemyAOCs = new AnimatorOverrideController[10];

    private void Awake()
    {
        enemySprites[0] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_green");
        enemySprites[1] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_yellow");
        enemySprites[2] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_red");
        enemySprites[3] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_blue");
        enemySprites[4] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_lime");
        enemySprites[5] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_brown");
        enemySprites[6] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_purple");
        enemySprites[7] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_azure");
        enemySprites[8] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_black");
        enemySprites[9] = Resources.Load<Sprite>("Sprites/Enemy/sprite_enemy_white");

        enemyAOCs[0] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_green");
        enemyAOCs[1] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_yellow");
        enemyAOCs[2] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_red");
        enemyAOCs[3] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_blue");
        enemyAOCs[4] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_lime");
        enemyAOCs[5] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_brown");
        enemyAOCs[6] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_purple");
        enemyAOCs[7] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_azure");
        enemyAOCs[8] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_black");
        enemyAOCs[9] = Resources.Load<AnimatorOverrideController>("Animations/Enemy/aoc_enemy_white");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(Type type, Tower tower, bool isOriginal)
    {
        if (isOriginal)
            tower.weapon.originalType = type;
        tower.weapon.currentType = type;
        tower.spriteRenderer.color = typeColors[(int)type];
    }

    public void SetType(Type type, Enemy enemy)
    {
        enemy.type = type;
        int typeNumber = (int)type;
        enemy.spriteRenderer.sprite = enemySprites[typeNumber];
        enemy.animator.runtimeAnimatorController = enemyAOCs[typeNumber];
    }

    public void SetType(Type type, Projectile projectile)
    {
        projectile.type = type;
        projectile.SpriteRenderer.color = typeColors[(int)type];
    }

    public void ColorConnection(SpriteRenderer connection, Type type)
    {
        connection.color = typeColors[(int)type] + new Color(0.1f, 0.1f, 0.1f);
    }
}
