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

    [HideInInspector]
    public Sprite unselectedCellSprite;
    [HideInInspector]
    public Sprite hoveredCellSprite;
    [HideInInspector]
    public Sprite selectedCellSprite;
    [HideInInspector]
    public Sprite blockedCellSprite;
    [HideInInspector]
    public Sprite[] towerSprites = new Sprite[11];

    private Sprite[] enemySprites = new Sprite[10];
    private Sprite[] projectileSprites = new Sprite[10];
    private Sprite[] connectionSprites = new Sprite[6];
    private AnimatorOverrideController[] enemyAOCs = new AnimatorOverrideController[10];
    private AnimatorOverrideController[] projectileAOCs = new AnimatorOverrideController[10];

    private void Awake()
    {
        unselectedCellSprite = Resources.Load<Sprite>("Sprites/Cell/sprite_cell_unselected");
        hoveredCellSprite = Resources.Load<Sprite>("Sprites/Cell/sprite_cell_hover");
        selectedCellSprite = Resources.Load<Sprite>("Sprites/Cell/sprite_cell_selected");
        blockedCellSprite = Resources.Load<Sprite>("Sprites/Cell/sprite_cell_blocked");

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

        towerSprites[0] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_green");
        towerSprites[1] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_yellow");
        towerSprites[2] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_red");
        towerSprites[3] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_blue");
        towerSprites[4] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_lime");
        towerSprites[5] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_brown");
        towerSprites[6] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_purple");
        towerSprites[7] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_azure");
        towerSprites[8] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_black");
        towerSprites[9] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_white");
        towerSprites[10] = Resources.Load<Sprite>("Sprites/Tower/sprite_tower_unselected");

        projectileSprites[0] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_green");
        projectileSprites[1] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_yellow");
        projectileSprites[2] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_red");
        projectileSprites[3] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_blue");
        projectileSprites[4] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_lime");
        projectileSprites[5] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_brown");
        projectileSprites[6] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_purple");
        projectileSprites[7] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_azure");
        projectileSprites[8] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_black");
        projectileSprites[9] = Resources.Load<Sprite>("Sprites/Projectile/sprite_projectile_white");

        connectionSprites[0] = Resources.Load<Sprite>("Sprites/Link/sprite_link_lime");
        connectionSprites[1] = Resources.Load<Sprite>("Sprites/Link/sprite_link_brown");
        connectionSprites[2] = Resources.Load<Sprite>("Sprites/Link/sprite_link_purple");
        connectionSprites[3] = Resources.Load<Sprite>("Sprites/Link/sprite_link_azure");
        connectionSprites[4] = Resources.Load<Sprite>("Sprites/Link/sprite_link_black");
        connectionSprites[5] = Resources.Load<Sprite>("Sprites/Link/sprite_link_white");

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

        projectileAOCs[0] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_green");
        projectileAOCs[1] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_yellow");
        projectileAOCs[2] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_red");
        projectileAOCs[3] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_blue");
        projectileAOCs[4] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_lime");
        projectileAOCs[5] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_brown");
        projectileAOCs[6] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_purple");
        projectileAOCs[7] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_azure");
        projectileAOCs[8] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_black");
        projectileAOCs[9] = Resources.Load<AnimatorOverrideController>("Animations/Projectile/aoc_projectile_white");
    }

    public void SetType(Type type, Tower tower, bool isOriginal)
    {
        if (isOriginal)
            tower.weapon.originalType = type;
        tower.weapon.currentType = type;
        tower.spriteRenderer.sprite = towerSprites[(int)type];
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
        int typeNumber = (int)type;
        projectile.spriteRenderer.sprite = projectileSprites[typeNumber];
        projectile.animator.runtimeAnimatorController = projectileAOCs[typeNumber];
    }

    public void ColorConnection(SpriteRenderer connection, Type type)
    {
        connection.sprite = connectionSprites[(int)type - 4];
    }
}
