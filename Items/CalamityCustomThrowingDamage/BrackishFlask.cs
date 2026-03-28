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
    public class BrackishFlask : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Brackish Flask");
            // Tooltip.SetDefault("Full of poisonous saltwater");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 28;
            Item.damage = 40;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 35;
            Item.useStyle = 1;
            Item.useTime = 35;
            Item.knockBack = 6.5f;
            Item.UseSound = SoundID.Item106;
            Item.autoReuse = true;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.shoot = Mod.Find<ModProjectile>("BrackishFlask").Type;
            Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "IOU");
            recipe.AddIngredient(null, "LivingShard");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
