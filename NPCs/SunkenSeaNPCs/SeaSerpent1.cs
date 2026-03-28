using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.WorldBuilding;

namespace CalamityModClassicPreTrailer.NPCs.SunkenSeaNPCs
{
	public class SeaSerpent1 : ModNPC
	{
        public bool flies = true;
        public const int maxLength = 9;
        public float speed = 6f;
        public float turnSpeed = 0.125f;
        bool TailSpawned = false;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sea Serpent");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/SeaSerpent_Bestiary",
                PortraitPositionXOverride = 40,
                PortraitPositionYOverride = 20
            };
            value.Position.Y += 20;
            value.Position.X += 40;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Mostly passive eels that drift along the still waters, if angered however, they prove quite the stubborn attackers!")
            });
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 50;
			NPC.width = 50; //42
			NPC.height = 24; //32
			NPC.defense = 10;
			NPC.lifeMax = 3000;
			NPC.aiStyle = -1;
            AIType = -1;
            NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 20, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.netAlways = true;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("SeaSerpentBanner").Type;
            SpawnModBiomes = new int[] { ModContent.GetInstance<SunkenSea>().Type };
		}
		
		public override void AI()
		{
			Lighting.AddLight(NPC.Center, ((255 - NPC.alpha) * 0f) / 255f, ((255 - NPC.alpha) * 0.30f) / 255f, ((255 - NPC.alpha) * 0.30f) / 255f);
            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            NPC.velocity.Length();
            if (Main.netMode != 1)
            {
                if (!TailSpawned && NPC.ai[0] == 0f)
                {
                    int Previous = NPC.whoAmI;
                    for (int segment = 0; segment < maxLength; segment++)
                    {
                        int lol = 0;
                        if (segment == 0 || segment == 1 || segment == 4 || segment == 5)
                        {
                            lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("SeaSerpent2").Type, NPC.whoAmI);
                        }
                        else if (segment == 2 || segment == 3 || segment == 6)
                        {
                            lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("SeaSerpent3").Type, NPC.whoAmI);
                        }
						else if (segment == 7)
						{
                            lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("SeaSerpent4").Type, NPC.whoAmI);
                        }
						else if (segment == 8)
						{
                            lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("SeaSerpent5").Type, NPC.whoAmI);
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
            int num180 = (int)(NPC.position.X / 16f) - 1;
            int num181 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
            int num182 = (int)(NPC.position.Y / 16f) - 1;
            int num183 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
            if (num180 < 0)
            {
                num180 = 0;
            }
            if (num181 > Main.maxTilesX)
            {
                num181 = Main.maxTilesX;
            }
            if (num182 < 0)
            {
                num182 = 0;
            }
            if (num183 > Main.maxTilesY)
            {
                num183 = Main.maxTilesY;
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
            }
            else if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = 1;
            }
            NPC.localAI[1] = 0f;
            bool canFly = flies;
            if (Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(false);
            }
            NPC.alpha -= 42;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 5600f || !NPC.AnyNPCs(Mod.Find<ModNPC>("SeaSerpent5").Type))
            {
                NPC.active = false;
            }
            float num188 = speed;
            float num189 = turnSpeed;
            Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
            float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
            int num42 = -1;
            int num43 = (int)(Main.player[NPC.target].Center.X / 16f);
            int num44 = (int)(Main.player[NPC.target].Center.Y / 16f);
            for (int num45 = num43 - 2; num45 <= num43 + 2; num45++)
            {
                for (int num46 = num44; num46 <= num44 + 15; num46++)
                {
                    if (WorldGen.SolidTile2(num45, num46))
                    {
                        num42 = num46;
                        break;
                    }
                }
                if (num42 > 0)
                {
                    break;
                }
            }
            if (num42 > 0)
            {
                num42 *= 16;
                float num47 = (float)(num42 - 200); //800
                if ((Main.player[NPC.target].Center - NPC.Center).Length() > 250f)
                {
                    num192 = num47;
                    if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 250f)
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            num191 = Main.player[NPC.target].Center.X + 300f;
                        }
                        else
                        {
                            num191 = Main.player[NPC.target].Center.X - 300f;
                        }
                    }
                }
            }
            float num48 = num188 * 1.3f;
            float num49 = num188 * 0.7f;
            float num50 = NPC.velocity.Length();
            if (num50 > 0f)
            {
                if (num50 > num48)
                {
                    NPC.velocity.Normalize();
                    NPC.velocity *= num48;
                }
                else if (num50 < num49)
                {
                    NPC.velocity.Normalize();
                    NPC.velocity *= num49;
                }
            }
            if ((Main.player[NPC.target].Center - NPC.Center).Length() > 250f)
            {
                for (int num51 = 0; num51 < 200; num51++)
                {
                    if (Main.npc[num51].active && Main.npc[num51].type == NPC.type && num51 != NPC.whoAmI)
                    {
                        Vector2 vector3 = Main.npc[num51].Center - NPC.Center;
                        if (vector3.Length() < 400f)
                        {
                            vector3.Normalize();
                            vector3 *= 1000f;
                            num191 -= vector3.X;
                            num192 -= vector3.Y;
                        }
                    }
                }
            }
            else
            {
                for (int num52 = 0; num52 < 200; num52++)
                {
                    if (Main.npc[num52].active && Main.npc[num52].type == NPC.type && num52 != NPC.whoAmI)
                    {
                        Vector2 vector4 = Main.npc[num52].Center - NPC.Center;
                        if (vector4.Length() < 60f)
                        {
                            vector4.Normalize();
                            vector4 *= 200f;
                            num191 -= vector4.X;
                            num192 -= vector4.Y;
                        }
                    }
                }
            }
            num191 = (float)((int)(num191 / 16f) * 16);
            num192 = (float)((int)(num192 / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
            if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    num191 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
                    num192 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                int num194 = NPC.width;
                num193 = (num193 - (float)num194) / num193;
                num191 *= num193;
                num192 *= num193;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num191;
                NPC.position.Y = NPC.position.Y + num192;
                if (num191 < 0f)
                {
                    NPC.spriteDirection = -1;
                }
                else if (num191 > 0f)
                {
                    NPC.spriteDirection = 1;
                }
            }
            else
            {
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                float num196 = System.Math.Abs(num191);
                float num197 = System.Math.Abs(num192);
                float num198 = num188 / num193;
                num191 *= num198;
                num192 *= num198;
                if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
                {
                    if (NPC.velocity.X < num191)
                    {
                        NPC.velocity.X = NPC.velocity.X + num189;
                    }
                    else
                    {
                        if (NPC.velocity.X > num191)
                        {
                            NPC.velocity.X = NPC.velocity.X - num189;
                        }
                    }
                    if (NPC.velocity.Y < num192)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num189;
                    }
                    else
                    {
                        if (NPC.velocity.Y > num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num189;
                        }
                    }
                    if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
                        }
                    }
                    if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189 * 2f; //changed from 2
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num189 * 2f; //changed from 2
                        }
                    }
                }
                else
                {
                    if (num196 > num197)
                    {
                        if (NPC.velocity.X < num191)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189 * 1.1f; //changed from 1.1
                        }
                        else if (NPC.velocity.X > num191)
                        {
                            NPC.velocity.X = NPC.velocity.X - num189 * 1.1f; //changed from 1.1
                        }
                        if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num189;
                            }
                            else
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189;
                            }
                        }
                    }
                    else
                    {
                        if (NPC.velocity.Y < num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
                        }
                        else if (NPC.velocity.Y > num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
                        }
                        if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + num189;
                            }
                            else
                            {
                                NPC.velocity.X = NPC.velocity.X - num189;
                            }
                        }
                    }
                }
            }
            NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea && spawnInfo.Water &&
				!NPC.AnyNPCs(Mod.Find<ModNPC>("SeaSerpent1").Type) && !spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().clamity && !spawnInfo.PlayerSafe)
            {
                return SpawnCondition.CaveJellyfish.Chance * 0.3f;
            }
            return 0f;
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Serpentine").Type, 4));
        }
        
        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 37, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 37, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
                if (Main.netMode != NetmodeID.Server)
				    Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity, Mod.Find<ModGore>("SeaSerpentGore1").Type, 1f);
			}
		}

        public override bool CheckActive()
        {
            if (NPC.timeLeft <= 0 && Main.netMode != 1)
            {
                for (int k = (int)NPC.ai[0]; k > 0; k = (int)Main.npc[k].ai[0])
                {
                    if (Main.npc[k].active)
                    {
                        Main.npc[k].active = false;
                        if (Main.netMode == 2)
                        {
                            Main.npc[k].life = 0;
                            Main.npc[k].netSkip = -1;
                            NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
                        }
                    }
                }
            }
            return true;
        }
	}
}