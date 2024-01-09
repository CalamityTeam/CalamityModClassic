using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class Omniblade : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 100;
			Item.damage = 90;
			Item.crit += 45;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 7;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 7;
			Item.useTurn = true;
			Item.knockBack = 6f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 130;
			Item.value = 1500000;
			Item.rare = ItemRarityID.Yellow;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			if (Main.rand.NextBool(5))
			{
				target.AddBuff(Mod.Find<ModBuff>("WhisperingDeath").Type, 360);
			}
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "BarofLife", 20);
			recipe.AddIngredient(null, "CoreofCalamity", 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
