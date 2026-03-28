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
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class Piggy : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Piggy");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new FlavorTextBestiaryInfoElement("The pig (Sus domesticus), also called swine or hog, is an omnivorous, domesticated, even-toed, hoofed mammal. It is named the domestic pig when distinguishing it from other members of the genus Sus. Some authorities consider it a subspecies of Sus scrofa (the wild boar or Eurasian boar); other authorities consider it a distinct species. Pigs were domesticated in the Neolithic, both in China and in the Near East (around the Tigris Basin). When domesticated pigs arrived in Europe, they extensively interbred with wild boar but retained their domesticated features. Pigs are farmed primarily for meat, called pork. The animal's skin or hide is used for leather. China is the world's largest pork producer, followed by the European Union and then the United States. Around 1.5 billion pigs are raised each year, producing some 120 million tonnes of meat, often cured as bacon. Some are kept as pets. Pigs have featured in human culture since Neolithic times, appearing in art and literature for children and adults, and celebrated in cities such as Bologna for their meat products.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.chaseable = false;
			NPC.aiStyle = 26;
			NPC.damage = 0;
			NPC.width = 26;
			NPC.height = 26;
			NPC.defense = 0;
			NPC.lifeMax = 2000;
			NPC.aiStyle = 7;
			AIType = 299;
			NPC.knockBackResist = 0.99f;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			/*banner = npc.type;
			bannerItem = mod.ItemType("PitbullBanner");*/
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.TownCritter.Chance * 0.01f;
		}

		public override void FindFrame(int frameHeight)
		{
			if (NPC.velocity.Y == 0f)
			{
				if (NPC.direction == 1)
				{
					NPC.spriteDirection = -1;
				}
				if (NPC.direction == -1)
				{
					NPC.spriteDirection = 1;
				}
				if (NPC.velocity.X == 0f)
				{
					NPC.frame.Y = 0;
					NPC.frameCounter = 0.0;
					return;
				}
				NPC.frameCounter += (double)(Math.Abs(NPC.velocity.X) * 0.25f);
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y / frameHeight >= Main.npcFrameCount[NPC.type] - 1)
				{
					NPC.frame.Y = frameHeight;
				}
			}
			else
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = frameHeight * 2;
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 15; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}