using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.IceMass
{
	public class IceMass : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Aurora Spirit");
			//Tooltip.SetDefault("Aurora Spirit");
			NPC.npcSlots = 0.5f;
			NPC.aiStyle = 86;
			NPC.damage = 66;
			NPC.width = 40; //324
			NPC.height = 24; //216
			NPC.defense = 5;
			NPC.alpha = 100;
			NPC.lifeMax = 80;
			NPC.knockBackResist = 0f;
			AnimationType = 472;
			Main.npcFrameCount[NPC.type] = 6;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath15;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("A spirit of an explorer who died while exploring in the tundra.")

            });
        }
        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.01f, 0.35f, 0.35f);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Frostburn, 100, true);
			}
			else
			{
				target.AddBuff(BuffID.Frostburn, 100, true);
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}