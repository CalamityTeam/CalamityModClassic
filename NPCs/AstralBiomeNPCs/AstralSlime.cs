using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs
{
    public class AstralSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Slime");
            Main.npcFrameCount[NPC.type] = 2;
        }
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Despite the mutation these slimes remain mostly unaffected, and behave identically to their kin.")
            });
        }

        public override void SetDefaults()
        {
            NPC.damage = 65;
            NPC.width = 44;
            NPC.height = 28;
            NPC.aiStyle = 1;
            NPC.defense = 18;
            NPC.lifeMax = 310;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.alpha = 60;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AnimationType = NPCID.BlueSlime;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("AstralSlimeBanner").Type;
            SpawnModBiomes = new int[] { ModContent.GetInstance<Astral>().Type };
		}

        public override void FindFrame(int frameHeight)
        {
            //DO DUST
            Dust d = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 44, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(4, 4, 36, 24), Vector2.Zero, 0.15f, true);
            if (d != null)
            {
                d.customData = 0.04f;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            CalamityGlobalNPC.DoHitDust(NPC, hit.HitDirection, ModContent.DustType<AstralOrange>(), 1f, 4, 24);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAstral && !spawnInfo.Player.ZoneRockLayerHeight)
            {
                return 0.21f;
            }
            return 0f;
        }
    }
}
