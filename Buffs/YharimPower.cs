using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
	public class YharimPower : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Yharim's Power");
			//Description.SetDefault("You feel like you can break the world in two...with your bare hands!");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer>().yPower = true;
		}
	}
}