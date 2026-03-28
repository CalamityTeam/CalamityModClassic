using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class PhantomSpiritM : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phantom Spirit");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				new FlavorTextBestiaryInfoElement("One of the many spirits imprisoned within the dungeon, long has it been consumed by everlasting hatred for the one responsible.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 90;
			NPC.width = 32; //324
			NPC.height = 32; //216
            NPC.scale = 1.1f;
			NPC.defense = 70;
			NPC.lifeMax = 2100;
			NPC.knockBackResist = 0.1f;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 40, 0);
			NPC.HitSound = SoundID.NPCHit36;
			NPC.DeathSound = SoundID.NPCDeath39;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			Banner = Mod.Find<ModNPC>("PhantomSpirit").Type;
			BannerItem = Mod.Find<ModItem>("PhantomSpiritBanner").Type;
		}

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
		{
			NPC.TargetClosest(true);
			Vector2 vector102 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num818 = Main.player[NPC.target].Center.X - vector102.X;
			float num819 = Main.player[NPC.target].Center.Y - vector102.Y;
			float num820 = (float)Math.Sqrt((double)(num818 * num818 + num819 * num819));
			float num821 = 13.5f;
			num820 = num821 / num820;
			num818 *= num820;
			num819 *= num820;
			NPC.velocity.X = (NPC.velocity.X * 100f + num818) / 101f;
			NPC.velocity.Y = (NPC.velocity.Y * 100f + num819) / 101f;
			NPC.rotation = (float)Math.Atan2((double)num819, (double)num818) - 1.57f;
			int num822 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 60, 0f, 0f, 0, default(Color), 1f);
			Dust dust = Main.dust[num822];
			dust.velocity *= 0.1f;
			Main.dust[num822].scale = 1.3f;
			Main.dust[num822].noGravity = true;
			return;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 150);
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 60, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int num288 = 0; num288 < 50; num288++)
				{
					int num289 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 60, NPC.velocity.X, NPC.velocity.Y, 0, default(Color), 1f);
					Dust dust = Main.dust[num289];
					dust.velocity *= 2f;
					Main.dust[num289].noGravity = true;
					Main.dust[num289].scale = 1.4f;
				}
			}
		}
		
		public override Color? GetAlpha(Color drawColor)
		{
			return new Color(200, 200, 200, 0);
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Phantoplasm").Type, 1, 1, 4));
		}
	}
}