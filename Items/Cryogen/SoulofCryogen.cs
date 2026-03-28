using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Cryogen
{
    [AutoloadEquip(EquipType.Wings)]
    public class SoulofCryogen : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul of Cryogen");
            /* Tooltip.SetDefault("The magic of the ancient ice castle is yours\n" +
                "Counts as wings\n" +
                "Horizontal speed: 6.25\n" +
                "Acceleration multiplier: 1\n" +
                "Average vertical speed\n" +
                "Flight time: 100\n" +
                "5% increase to all damage and pick speed\n" +
				"All melee attacks and projectiles inflict frostburn"); */
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(100, 6.25f);
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 39, 99, 99);
            Item.expert = true;
            Item.accessory = true;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            maxFallSpeed *= 0f;
            float num = (float)Main.rand.Next(90, 111) * 0.01f;
            num *= Main.essScale;
            Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0f * num, 0.3f * num, 0.3f * num);
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.5f;
            ascentWhenRising = 0.1f;
            maxCanAscendMultiplier = 0.5f;
            maxAscentMultiplier = 1.5f;
            constantAscend = 0.1f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            CalamityCustomThrowingDamagePlayer modPlayer2 = CalamityCustomThrowingDamagePlayer.ModPlayer(player);
            modPlayer.cryogenSoul = true;
            player.pickSpeed -= 0.05f;
            player.GetDamage(DamageClass.Magic) += 0.05f;
            player.GetDamage(DamageClass.Ranged) += 0.05f;
            player.GetDamage(DamageClass.Melee) += 0.05f;
            player.GetDamage(DamageClass.Summon) += 0.05f;
            modPlayer2.throwingDamage += 0.05f;
            player.wingTimeMax = 100;
        }
    }
}