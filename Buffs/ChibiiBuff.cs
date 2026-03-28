using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class ChibiiBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chibii Devourer");
			// Description.SetDefault("What? Were you expecting someone else?");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;

            player.GetModPlayer<CalamityPlayerPreTrailer>().chibii = true;

			bool petProjectileNotSpawned = player.ownedProjectileCounts[Mod.Find<ModProjectile>("ChibiiDoggo").Type] <= 0;

			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex),player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, Mod.Find<ModProjectile>("ChibiiDoggo").Type, 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
