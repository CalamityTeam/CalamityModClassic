using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.BrimstoneWaifu
{
    public class RoseStone : ModItem
    {
    	public override void SetStaticDefaults()
	 	{
	 		// DisplayName.SetDefault("Rose Stone");
	 		/* Tooltip.SetDefault("One of the ancient relics\n" +
            	"Increases max life by 20, life regen by 1, and all damage by 3%\n" +
            	"Summons a brimstone elemental to fight for you"); */
	 	}
    	
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
			Item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (modPlayer.elementalHeart)
            {
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
        	Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.6f, 0f, 0.25f);
			player.lifeRegen += 1;
			player.statLifeMax2 += 20;
			player.GetDamage(DamageClass.Melee) += 0.03f;
			player.GetDamage(DamageClass.Magic) += 0.03f;
			player.GetDamage(DamageClass.Ranged) += 0.03f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.03f;
			player.GetDamage(DamageClass.Summon) += 0.03f;
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.brimstoneWaifu = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("BrimstoneWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("BrimstoneWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] < 1)
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("BigBustyRose").Type, (int)(45f * player.GetDamage(DamageClass.Summon).Multiplicative), 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}
    }
}
