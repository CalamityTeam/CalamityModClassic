using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Impaler : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Impaler");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Impaler");
        Item.damage = 82;
        Item.DamageType = DamageClass.Ranged;
        Item.crit += 10;
        Item.width = 40;
        Item.height = 26;
        ////Tooltip.SetDefault("Fires exploding and flaming stakes\nVlad would be proud!");
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7f;
        Item.value = 1050000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("FlamingStake").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 10f;
        Item.useAmmo = 1836;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, -10);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
        float SpeedX = velocity.X + (float) Main.rand.Next(-5, 6) * 0.05f;
        float SpeedY = velocity.Y + (float) Main.rand.Next(-5, 6) * 0.05f;
        if (Main.rand.Next(3) == 0)
        {
        	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),Mod.Find<ModProjectile>("ExplodingStake").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        else
        {
        	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),Mod.Find<ModProjectile>("FlamingStake").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        }
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CoreofChaos", 5);
        recipe.AddIngredient(ItemID.StakeLauncher);
        recipe.AddIngredient(ItemID.ExplosivePowder, 100);
        recipe.AddIngredient(ItemID.LivingFireBlock, 75);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}