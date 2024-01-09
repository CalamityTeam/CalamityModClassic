using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TrueCausticEdge : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("True Caustic Edge");
			//Tooltip.SetDefault("Pestilent Defilement");
		}

	public override void SetDefaults()
	{
		Item.width = 54;  //The width of the .png file in pixels divided by 2.
		Item.damage = 42;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 28;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 28;
		Item.useTurn = true;
		Item.knockBack = 5.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 68;  //The height of the .png file in pixels divided by 2.
		Item.value = 635000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TrueCausticEdgeProjectile").Type;
		Item.shootSpeed = 16f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CausticEdge");
		recipe.AddIngredient(ItemID.FlaskofCursedFlames, 5);
		recipe.AddIngredient(ItemID.FlaskofPoison, 5);
		recipe.AddIngredient(ItemID.Deathweed, 3);
		recipe.AddTile(TileID.DemonAltar);	
		recipe.Register();
		recipe = CreateRecipe();
		recipe.AddIngredient(null, "CausticEdge");
		recipe.AddIngredient(ItemID.FlaskofIchor, 5);
		recipe.AddIngredient(ItemID.FlaskofPoison, 5);
		recipe.AddIngredient(ItemID.Deathweed, 3);
		recipe.AddTile(TileID.DemonAltar);	
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
		target.AddBuff(BuffID.OnFire, 300);
		target.AddBuff(BuffID.Venom, 180);
	}
}}
