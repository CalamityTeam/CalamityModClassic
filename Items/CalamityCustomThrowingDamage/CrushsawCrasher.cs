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
    public class CrushsawCrasher : CalamityDamageItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crushsaw Crasher");
        }

        public override void SafeSetDefaults()
        {
            Item.width = 38;
            Item.damage = 65;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.useTime = 18;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 22;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.shoot = Mod.Find<ModProjectile>("Crushax").Type;
            Item.shootSpeed = 11f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
        }
    }
}
