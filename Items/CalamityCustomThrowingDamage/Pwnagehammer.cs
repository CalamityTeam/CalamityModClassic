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
	public class Pwnagehammer : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pwnagehammer");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 68;
			Item.damage = 90;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.useTime = 15;
			Item.knockBack = 10f;
			Item.UseSound = SoundID.Item1;
			Item.height = 68;
            Item.value = Item.buyPrice(0, 48, 0, 0);
            Item.rare = 6;
			Item.shoot = Mod.Find<ModProjectile>("Pwnagehammer").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Pwnhammer);
			recipe.AddIngredient(ItemID.HallowedBar, 7);
            recipe.AddIngredient(ItemID.SoulofMight, 3);
            recipe.AddIngredient(ItemID.SoulofSight, 3);
            recipe.AddIngredient(ItemID.SoulofFright, 3);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
