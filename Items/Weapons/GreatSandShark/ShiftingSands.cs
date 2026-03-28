using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.GreatSandShark
{
    public class ShiftingSands : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shifting Sands");
            // Tooltip.SetDefault("Casts a sand shard that follows the mouse cursor");
        }

        public override void SetDefaults()
        {
            Item.damage = 95;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 20;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item20;
            Item.shoot = Mod.Find<ModProjectile>("ShiftingSands").Type;
            Item.shootSpeed = 7f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicMissile);
            recipe.AddIngredient(null, "GrandScale");
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
            recipe.AddIngredient(ItemID.SpectreBar, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}