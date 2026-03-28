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
    public class SpearofPaleolith : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spear of Paleolith");
            // Tooltip.SetDefault("Throws an ancient spear that shatters enemy armor at high velocity\nSpears rain fossil shards as they travel");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 54;
            Item.damage = 65;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 27;
            Item.useStyle = 1;
            Item.useTime = 27;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 54;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.shoot = Mod.Find<ModProjectile>("SpearofPaleolith").Type;
            Item.shootSpeed = 35f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
            recipe.AddIngredient(ItemID.AdamantiteBar, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
            recipe.AddIngredient(ItemID.TitaniumBar, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
