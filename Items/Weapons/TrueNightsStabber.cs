using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TrueNightsStabber : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("True Night's Stabber");
			//Tooltip.SetDefault("Don't underestimate the power of stabby knives");
		}

	public override void SetDefaults()
	{
		Item.useStyle = ItemUseStyleID.Thrust;
		Item.useTurn = false;
		Item.useAnimation = 16;
		Item.useTime = 16;  //Ranges from 1 to 55.
		Item.width = 40;  //The width of the .png file in pixels divided by 2.
		Item.height = 40;  //The height of the .png file in pixels divided by 2.
		Item.damage = 75;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.knockBack = 6.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.useTurn = true;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.shoot = Mod.Find<ModProjectile>("ShortNightBeam").Type;
		Item.shootSpeed = 8f;
		Item.value = 1000000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "NightsStabber");
		recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddTile(TileID.MythrilAnvil);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.VilePowder);
        }
    }
}}
