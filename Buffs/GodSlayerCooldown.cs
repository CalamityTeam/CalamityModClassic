using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
	public class GodSlayerCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("God Slayer Cooldown");
			//Description.SetDefault("10% increase to all damage; godslayer effect is recharging");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer>().godSlayerCooldown = true;
		}
	}
}