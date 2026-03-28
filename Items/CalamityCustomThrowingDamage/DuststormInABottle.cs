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
    public class DuststormInABottle : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Duststorm in a Bottle");
            // Tooltip.SetDefault("Explodes into a dust cloud");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 20;
            Item.damage = 47;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 25;
            Item.useStyle = 1;
            Item.useTime = 25;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item106;
            Item.autoReuse = true;
            Item.height = 24;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.shoot = Mod.Find<ModProjectile>("DuststormInABottle").Type;
            Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HolyWater, 20);
            recipe.AddIngredient(null, "GrandScale");
            recipe.AddIngredient(ItemID.SandstorminaBottle);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
