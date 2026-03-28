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
    public class ProfanedTrident : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infernal Spear");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 72;
            Item.damage = 1700;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 13;
            Item.useStyle = 1;
            Item.useTime = 13;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 72;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("ProfanedTrident").Type;
            Item.shootSpeed = 28f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
    }
}
