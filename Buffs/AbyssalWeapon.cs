using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
	public class AbyssalWeapon : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Abyssal Weapon");
			//Description.SetDefault("Melee weapons inflict abyssal flames, 15% increased movement speed");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer1Point2>().aWeapon = true;
		}
	}
}