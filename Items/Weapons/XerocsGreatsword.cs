using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class XerocsGreatsword : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Xeroc Greatsword");
		//Tooltip.SetDefault("Fires homing plasma balls");
	}

	public override void SetDefaults()
	{
		Item.width = 64;  //The width of the .png file in pixels divided by 2.
		Item.damage = 113;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 25;
		Item.useTurn = true;
		Item.knockBack = 5.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 64;  //The height of the .png file in pixels divided by 2.
		Item.value = 400000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Cyan;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("PlasmaBall").Type;
		Item.shootSpeed = 12f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
		float SpeedA = velocity.X;
   		float SpeedB = velocity.Y;
        int num6 = Main.rand.Next(4, 5);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = velocity.X;
            float num8 = velocity.Y;
            float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.75), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        return false;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "MeldiateBar", 15);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Shadowflame);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.CursedInferno, 500);
	}
}}
