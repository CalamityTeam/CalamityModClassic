using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage.Patreon
{
	public class Plaguenade : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plaguenade");
			// Tooltip.SetDefault("Releases a swarm of angry plague bees");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 20;
			Item.damage = 60;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.useAnimation = 12;
			Item.useStyle = 1;
			Item.useTime = 12;
			Item.knockBack = 1.5f;
			Item.maxStack = 999;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 28;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("Plaguenade").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(30);
            recipe.AddIngredient(ItemID.Beenade, 15);
            recipe.AddIngredient(null, "PlagueCellCluster", 3);
			recipe.AddIngredient(ItemID.Obsidian, 2);
			recipe.AddIngredient(ItemID.Stinger);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
