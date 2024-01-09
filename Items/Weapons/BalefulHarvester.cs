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
	public class BalefulHarvester : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Baleful Harvester");
		}

		public override void SetDefaults()
		{
			Item.damage = 105;
			Item.width = 54;
			Item.height = 54;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 32;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 32;
			Item.useTurn = true;
			Item.knockBack = 8;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.value = 475000;
			Item.rare = ItemRarityID.Lime;
			Item.shoot = Mod.Find<ModProjectile>("BalefulHarvesterProjectile").Type;
			Item.shootSpeed = 6f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Pumpkin, 30);
			recipe.AddIngredient(ItemID.BookofSkulls);
	        recipe.AddIngredient(ItemID.SpookyWood, 200);
	        recipe.AddIngredient(ItemID.TheHorsemansBlade);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.OnFire, 2400);
		}
	}
}
