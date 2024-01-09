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
	public class BladeofEnmity : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Blade of Enmity");
		}

		public override void SetDefaults()
		{
			Item.width = 60;
			Item.damage = 150;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useTurn = true;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 68;
			Item.value = 1250000;
			Item.rare = ItemRarityID.Yellow;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "BarofLife", 5);
			recipe.AddIngredient(null, "CoreofCalamity", 3);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 2000);
		}
	}
}
