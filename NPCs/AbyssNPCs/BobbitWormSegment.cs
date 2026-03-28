using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class BobbitWormSegment : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bobbit Worm");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.alpha = 255;
			NPC.width = 26; //324
			NPC.height = 26; //216
			NPC.defense = 0;
			NPC.lifeMax = 100;
			NPC.knockBackResist = 0f;
			AIType = -1;
			NPC.dontTakeDamage = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}

		public override void AI()
		{
			CalamityGlobalNPC.bobbitWormBottom = NPC.whoAmI;
			if (Main.netMode != 1)
			{
				if (NPC.localAI[0] == 0f)
				{
					NPC.localAI[0] = 1f;
					NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("BobbitWormHead").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				}
			}
			if (!NPC.AnyNPCs(Mod.Find<ModNPC>("BobbitWormHead").Type))
			{
				NPC.active = false;
				NPC.netUpdate = true;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer4 && spawnInfo.Water)
			{
				if (!NPC.AnyNPCs(Mod.Find<ModNPC>("BobbitWormSegment").Type))
					return SpawnCondition.CaveJellyfish.Chance * 0.3f;
			}
			return 0f;
		}

		public override bool CheckActive()
		{
			return false;
		}
	}
}