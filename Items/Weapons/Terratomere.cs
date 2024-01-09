using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Terratomere : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 56;  //The width of the .png file in pixels divided by 2.
		Item.damage = 105;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 20;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 20;
		Item.useTurn = true;
		Item.knockBack = 7f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 56;  //The height of the .png file in pixels divided by 2.
		Item.value = 2400000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Red;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TerratomereProjectile").Type;
		Item.shootSpeed = 20f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int num6 = Main.rand.Next(4, 6);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = velocity.X;
            float num8 = velocity.Y;
            float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.85f), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        return false;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "XerocsGreatsword");
		recipe.AddIngredient(null, "Mariana");
		recipe.AddIngredient(null, "Hellkite");
		recipe.AddIngredient(null, "TemporalFloeSword");
		recipe.AddIngredient(ItemID.TerraBlade);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(null, "XerocsGreatsword");
		recipe.AddIngredient(null, "Mariana");
		recipe.AddIngredient(null, "Hellkite");
		recipe.AddIngredient(null, "TemporalFloeSword");
		recipe.AddIngredient(null, "TerraEdge");
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GreenFairy);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	if (Main.rand.NextBool(3))
    	{
    		target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 300);
    	}
		target.AddBuff(BuffID.CursedInferno, 680);
		target.AddBuff(BuffID.Frostburn, 620);
		target.AddBuff(BuffID.OnFire, 600);
		target.AddBuff(BuffID.Ichor, 300);
		if (target.type == NPCID.TargetDummy)
		{
			return;
		}
		int healAmount = (Main.rand.Next(3) + 2);
    	player.statLife += healAmount;
    	player.HealEffect(healAmount);
	}
}}
