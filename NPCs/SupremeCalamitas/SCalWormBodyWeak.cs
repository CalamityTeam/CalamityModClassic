using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.SupremeCalamitas
{
	public class SCalWormBodyWeak : ModNPC
	{
        public double damageTaken = 0.0;
        public int invinceTime = 360;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimstone Heart");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
        
		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			modifiers.SetMaxDamage(1);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 0; //70
			NPC.npcSlots = 5f;
			NPC.width = 14; //324
			NPC.height = 12; //216
			NPC.defense = 0;
            NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 1200000 : 1000000;
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 2000000;
            }
            NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.2f;
			if (Main.expertMode)
			{
				NPC.scale = 1.35f;
			}
			NPC.alpha = 255;
            NPC.chaseable = false;
            NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit13;
			NPC.DeathSound = SoundID.NPCDeath13;
			NPC.netAlways = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.dontCountMe = true;
		}

        public override void AI()
        {
            if (Main.npc[(int)NPC.ai[1]].alpha < 128)
            {
                NPC.alpha -= 42;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }
            if (Main.netMode != 1)
            {
                NPC.localAI[0] += 1f;
                if (NPC.localAI[0] >= 900f)
                {
                    NPC.localAI[0] = 0f;
                    int damage = Main.expertMode ? 150 : 200;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 1f, 1f, Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage, 0f, NPC.target, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, -1f, 1f, Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage, 0f, NPC.target, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 1f, -1f, Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage, 0f, NPC.target, 0f, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, -1f, -1f, Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage, 0f, NPC.target, 0f, 0f);
                    NPC.netUpdate = true;
                }
            }
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
        }

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if ((projectile.penetrate == -1 || projectile.penetrate > 1) && !projectile.minion)
			{
				projectile.penetrate = 1;
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
	}
}