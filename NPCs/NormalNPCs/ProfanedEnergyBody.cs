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
	public class ProfanedEnergyBody : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Profaned Energy");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers
			{
				PortraitPositionYOverride = 0f,
				CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/NormalNPCs/ProfanedEnergy_Bestiary"
			};
			value.Position.Y += 30f;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("A living altar. Its flame burns fiercely to punish the unfaithful.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.npcSlots = 3f;
			NPC.width = 72; //324
			NPC.height = 36; //216
			NPC.defense = 58;
			NPC.lifeMax = 6000;
			NPC.knockBackResist = 0f;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 50, 0);
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath55;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("ProfanedEnergyBanner").Type;
		}
		
		public override void AI()
		{
			CalamityGlobalNPC.energyFlame = NPC.whoAmI;
			if (Main.netMode != 1) 
			{
				if (NPC.localAI[0] == 0f) 
				{
					NPC.localAI[0] = 1f;
					for (int num723 = 0; num723 < 2; num723++) 
					{
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("ProfanedEnergyLantern").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
					}
				}
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !NPC.downedMoonlord || NPC.AnyNPCs(NPC.type))
			{
				return 0f;
			}
			if (SpawnCondition.Underworld.Chance > 0f)
			{
				return SpawnCondition.Underworld.Chance / 6f;
			}
			return SpawnCondition.OverworldHallow.Chance / 6f;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("UnholyEssence").Type, 1, 2, 5));
		}
	}
}