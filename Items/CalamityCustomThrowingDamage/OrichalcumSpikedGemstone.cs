using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class OrichalcumSpikedGemstone : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Orichalcum Spiked Gemstone");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 14;
			Item.damage = 25;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 13;
			Item.useStyle = 1;
			Item.useTime = 13;
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 24;
			Item.shoot = 330;
			Item.maxStack = 999;
			Item.value = 1200;
			Item.rare = 4;
			Item.shoot = Mod.Find<ModProjectile>("OrichalcumSpikedGemstoneProjectile").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
	        recipe.AddIngredient(ItemID.OrichalcumBar);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
