using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TheSwarmer : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Swarmer");
		}

    public override void SetDefaults()
    {
        Item.damage = 36;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 7;
        Item.width = 74;
        Item.height = 36;
        Item.useTime = 8;
        Item.useAnimation = 8;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.value = 1000000;
        Item.rare = ItemRarityID.Cyan;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.Wasp; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 12f;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-15, -5);
	}
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FragmentVortex, 20);
		recipe.AddIngredient(ItemID.BeeGun);
		recipe.AddIngredient(ItemID.WaspGun);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
	    for (int i = 0; i <= 3; i++)
	    {
	    	float SpeedX = velocity.X + (float) Main.rand.Next(-35, 36) * 0.05f;
	    	float SpeedY = velocity.Y + (float) Main.rand.Next(-35, 36) * 0.05f;
	    	int wasps = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, 0f, player.whoAmI, 0.0f, 0.0f);
	    	Main.projectile[wasps].penetrate = 1;
	    }
	    for (int i = 0; i <= 3; i++)
	    {
	    	float SpeedX2 = velocity.X + (float) Main.rand.Next(-35, 36) * 0.05f;
	    	float SpeedY2 = velocity.Y + (float) Main.rand.Next(-35, 36) * 0.05f;
	    	int bees = Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, 181, damage, 0f, player.whoAmI, 0.0f, 0.0f);
	    	Main.projectile[bees].penetrate = 1;
	    }
	    return false;
	}
    
    
}}