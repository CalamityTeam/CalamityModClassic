using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point1.Tiles;
using Terraria.WorldBuilding;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.DriedSeeker
{
	public class DriedSeekerHead : ModNPC
	{
		bool TailSpawned = false;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "CalamityModClassic1Point1/NPCs/DriedSeeker/Bestiary"
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }
        public override void SetDefaults()
		{
			//NPC.name = "DriedSeekerHead");
			//Tooltip.SetDefault("Dried Seeker");
			NPC.damage = 18; //150
			NPC.npcSlots = 5f;
			NPC.width = 18; //324
			NPC.height = 18; //216
			NPC.defense = 0;
			NPC.lifeMax = 100; //250000
			NPC.aiStyle = 6;
			Main.npcFrameCount[NPC.type] = 1;
            AIType = -1;
            AnimationType = 10;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.netAlways = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                new FlavorTextBestiaryInfoElement("Smaller sea serpents displaced by the Great Sea's incineration.")

            });
        }

        public override void AI()
		{
			if (!TailSpawned)
            {
                int Previous = NPC.whoAmI;
                for (int num36 = 0; num36 < 5; num36++)
                {
                    int lol = 0;
                    if (num36 >= 0 && num36 < 4)
                    {
                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DriedSeekerBody").Type, NPC.whoAmI);
                    }
                    else
                    {
                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DriedSeekerTail").Type, NPC.whoAmI);
                    }
                    Main.npc[lol].realLife = NPC.whoAmI;
                    Main.npc[lol].ai[2] = (float)NPC.whoAmI;
                    Main.npc[lol].ai[1] = (float)Previous;
                    Main.npc[Previous].ai[0] = (float)lol;
                    NetMessage.SendData(23, -1, -1, null, lol, 0f, 0f, 0f, 0);
                    Previous = lol;
                }
                TailSpawned = true;
            }
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.75f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Bleeding, 200, true);
			}
		}
	}
}