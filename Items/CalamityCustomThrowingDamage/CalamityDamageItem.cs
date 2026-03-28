using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public abstract class CalamityDamageItem : ModItem
	{
		public virtual void SafeSetDefaults()
		{
		}

		public sealed override void SetDefaults()
		{
			SafeSetDefaults();
			Item.DamageType = DamageClass.Ranged;
		}

		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			damage.Base *= (float)(CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage + 5E-06f); //plus one otherwise weird shit happens
		}

		public override void ModifyWeaponCrit(Player player, ref float crit)
		{
			crit = (Item.crit + CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit); //change crit back to normal
		}

		public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
		{
			if (player.shroomiteStealth)
			{
				knockback /= 1f + (1f - player.stealth) * 0.5f;
			}
			if (player.setVortex)
			{
				knockback /= 1f + (1f - player.stealth) * 0.5f;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
			if (tt != null)
			{
				string[] splitText = tt.Text.Split(' ');
				string damageValue = splitText.First();
				string damageWord = splitText.Last();
				tt.Text = damageValue + " rogue " + damageWord;
			}
		}

		public override bool ConsumeItem(Player player)
		{
			if (CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingAmmoCost50)
			{
				if (Main.rand.Next(1, 101) > 50)
					return false;
			}
			if (CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingAmmoCost66)
			{
				if (Main.rand.Next(1, 101) > 66)
					return false;
			}
			return true;
		}
	}
}