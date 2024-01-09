using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.IceClasper
{
	public class IceClasper : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ice Clasper");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 5;
			NPC.noGravity = true;
			NPC.damage = 60;
			NPC.width = 40; //324
			NPC.height = 60; //216
			NPC.defense = 23;
			NPC.lifeMax = 320;
			NPC.knockBackResist = 0f;
			AnimationType = 6;
			AIType = NPCID.EaterofSouls;
			NPC.value = Item.buyPrice(0, 0, 15, 0);
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
			NPC.coldDamage = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("Watch your toes while you're treading through the snow biome, or one of these might bite you!")

            });
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.6f, 0.75f);
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneSnow && 
            	!spawnInfo.Player.ZoneTowerStardust &&
            	!spawnInfo.Player.ZoneTowerSolar &&
            	!spawnInfo.Player.ZoneTowerVortex &&
            	!spawnInfo.Player.ZoneTowerNebula &&
            	Main.hardMode && !spawnInfo.PlayerInTown && !spawnInfo.Player.ZoneOldOneArmy && !Main.snowMoon && !Main.pumpkinMoon ? 0.025f : 0f;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.75f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Frostburn, 300, true);
				target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 50, true);
			}
			else
			{
				target.AddBuff(BuffID.Frostburn, 160, true);
				target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 50, true);
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Frost, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Frost, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofEleum>(), 2));
        }
	}
}