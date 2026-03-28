using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class Backfire : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Backfire");
			// Description.SetDefault("Damage, defense, and wing time drastically reduced");
			Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) -= 0.9f;
            player.GetDamage(DamageClass.Magic) -= 0.9f;
            player.GetDamage(DamageClass.Ranged) -= 0.9f;
            player.GetDamage(DamageClass.Throwing) -= 0.9f;
            player.GetDamage(DamageClass.Summon) -= 0.9f;
            player.statDefense -= 20;
            player.endurance -= 0.2f;
            player.wingTimeMax /= 2;
        }
	}
}
