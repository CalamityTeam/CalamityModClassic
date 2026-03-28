using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class BalefulHarvester : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Baleful Harvester");
		}

		public override void SetDefaults()
		{
			Item.damage = 110;
			Item.width = 66;
			Item.height = 66;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 22;
			Item.useStyle = 1;
			Item.useTime = 22;
			Item.useTurn = true;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
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
