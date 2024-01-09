using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TyrantYharimsUltisword : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tyrant Yharim's Ultisword");
		//Tooltip.SetDefault("Necrotic blade of Jungle King Yharim\n50% chance to give the player the tyrant's fury buff on enemy hits\nThis buff increases melee damage, speed, and crit chance by 30%");
	}

	public override void SetDefaults()
	{
		Item.width = 84;  //The width of the .png file in pixels divided by 2.
		Item.damage = 60;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 26;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 26;
		Item.useTurn = true;
		Item.knockBack = 5.50f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 84;  //The height of the .png file in pixels divided by 2.
		Item.value = 1250000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Cyan;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("BlazingPhantomBlade").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TrueCausticEdge");
		recipe.AddIngredient(ItemID.TrueNightsEdge);
		recipe.AddIngredient(ItemID.FlaskofVenom, 5);
		recipe.AddIngredient(ItemID.FlaskofCursedFlames, 5);
		recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(null, "TrueCausticEdge");
		recipe.AddIngredient(null, "TrueBloodyEdge");
		recipe.AddIngredient(ItemID.FlaskofVenom, 5);
		recipe.AddIngredient(ItemID.FlaskofCursedFlames, 5);
		recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(null, "TrueCausticEdge");
		recipe.AddIngredient(ItemID.TrueNightsEdge);
		recipe.AddIngredient(ItemID.FlaskofVenom, 5);
		recipe.AddIngredient(ItemID.FlaskofIchor, 5);
		recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(null, "TrueCausticEdge");
		recipe.AddIngredient(null, "TrueBloodyEdge");
		recipe.AddIngredient(ItemID.FlaskofVenom, 5);
		recipe.AddIngredient(ItemID.FlaskofIchor, 5);
		recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.RuneWizard);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if (Main.rand.NextBool(2))
    	{
    		player.AddBuff(Mod.Find<ModBuff>("TyrantsFury").Type, 180);
    	}
		target.AddBuff(BuffID.OnFire, 300);
		target.AddBuff(BuffID.Venom, 240);
		target.AddBuff(BuffID.CursedInferno, 180);
	}
}}
