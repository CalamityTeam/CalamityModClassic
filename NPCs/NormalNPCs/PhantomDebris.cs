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
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class PhantomDebris : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phantom Debris");
			Main.npcFrameCount[NPC.type] = 4;
		}
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("A strange bug of unknown origin... seriously, i've got nothing on this one. Is it a bug animated by dungeon spirits? a buncha rocks?? just some bug??? what's up with it????")
            });
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 30;
			NPC.width = 44; //324
			NPC.height = 22; //216
			NPC.defense = 35;
			NPC.lifeMax = 80;
            NPC.aiStyle = 3;
            AIType = 67;
            NPC.knockBackResist = 0.5f;
			NPC.value = Item.buyPrice(0, 0, 2, 0);
			NPC.HitSound = SoundID.NPCHit2;
			NPC.DeathSound = SoundID.NPCDeath36;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("PhantomDebrisBanner").Type;
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
            NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
            float num78 = 1f;
            float num79 = (Main.player[NPC.target].Center - NPC.Center).Length();
            num79 *= 0.0025f;
            if ((double)num79 > 1.5)
            {
                num79 = 1.5f;
            }
            if (Main.expertMode)
            {
                num78 = 4f - num79;
            }
            else
            {
                num78 = 3f - num79;
            }
            num78 *= 0.8f;
            if (NPC.velocity.X < -num78 || NPC.velocity.X > num78)
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else if (NPC.velocity.X < num78 && NPC.direction == 1)
            {
                NPC.velocity.X = NPC.velocity.X + 1.5f;
                if (NPC.velocity.X > num78)
                {
                    NPC.velocity.X = num78;
                }
            }
            else if (NPC.velocity.X > -num78 && NPC.direction == -1)
            {
                NPC.velocity.X = NPC.velocity.X - 1.5f;
                if (NPC.velocity.X < -num78)
                {
                    NPC.velocity.X = -num78;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || !Main.hardMode || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyss ||
				spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
            {
                return 0f;
            }
            return SpawnCondition.Underground.Chance * 0.02f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (CalamityWorldPreTrailer.revenge)
            {
                target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 120);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 60, hit.HitDirection, -1f, 0, default(Color), 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 15; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 60, hit.HitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("AncientBoneDust").Type, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new DownedMoonLord(), Mod.Find<ModItem>("Phantoplasm").Type));
        }
	}
}