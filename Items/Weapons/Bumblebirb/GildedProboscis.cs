using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Bumblebirb
{
    public class GildedProboscis : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gilded Proboscis");
            // Tooltip.SetDefault("This spear ignores npc immunity frames\nHeals the player on enemy hits");
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.damage = 160;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 19;
            Item.useStyle = 5;
            Item.useTime = 19;
            Item.knockBack = 8.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 66;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("GildedProboscis").Type;
            Item.shootSpeed = 13f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
