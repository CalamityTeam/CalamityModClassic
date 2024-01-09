using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Trinity : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Trinity");
	}

	public override void SetDefaults()
	{
		Item.width = 44;  //The width of the .png file in pixels divided by 2.
		Item.damage = 50;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useTime = 25;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 4.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 46;  //The height of the .png file in pixels divided by 2.
		Item.value = 110000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Pink;  //Ranges from 1 to 11.
		Item.shoot = ProjectileID.RubyBolt;
		Item.shootSpeed = 11f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	switch (Main.rand.Next(6))
		{
    		case 1: type = 125; break;
    		case 2: type = 123; break;
    		case 3: type = 121; break;
    		default: break;
		}
    	for (int projectiles = 0; projectiles <= 3; projectiles++)
    	{
    		float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
       		int projectile1 = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.6f), knockback, Main.myPlayer);
       		Main.projectile[projectile1].DamageType = DamageClass.Melee;
    	}
    	return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VerstaltiteBar", 9);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PinkFairy);
        }
    }
}}
