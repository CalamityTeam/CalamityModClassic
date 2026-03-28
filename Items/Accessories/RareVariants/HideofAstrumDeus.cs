using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories.RareVariants
{
    public class HideofAstrumDeus : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hide of Astrum Deus");
            /* Tooltip.SetDefault("Taking damage drops an immense amount of astral stars from the sky and boosts true melee damage by 200% for a time\n" +
								"Boost duration is based on the amount of damage you took, the higher the damage the longer the boost\n" +
								"Provides immunity to the god slayer inferno, cursed inferno, on fire, and frostburn debuffs\n" +
								"Enemies take damage when they hit you and are inflicted with the god slayer inferno debuff"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.expert = true;
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.aBulwark = true;
			player.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = true;
			modPlayer.aBulwarkRare = true;
			player.buffImmune[BuffID.CursedInferno] = true;
			player.buffImmune[BuffID.OnFire] = true;
			player.buffImmune[BuffID.Frostburn] = true;
			player.thorns += 0.75f;
		}
	}
}