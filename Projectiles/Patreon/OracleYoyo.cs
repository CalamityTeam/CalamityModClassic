using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Patreon;

namespace CalamityModClassicPreTrailer.Projectiles.Patreon
{
    public class OracleYoyo : ModProjectile
    {
        // projectile.localAI[1] is the Aura Charge of the red lightning aura
        // Minimum value is zero. Maximum value is 200.
        // The aura turns on and begins damaging enemies at 20 charge.
        // The yoyo "supercharges" at 50 charge.
        // Its size caps out at 100 charge.
        private static float MaxCharge = 200f;
        private static float MinAuraRadius = 20f;
        private static float SuperchargeThreshold = 50f;
        private static float MaxAuraRadius = 100f;
        private static float MinDischargeRate = 0.05f;
        private static float MaxDischargeRate = 0.53f;
        private static float ChargePerHit = 6f;

        private static int AuraBaseDamage = 40;
        private static int HitsPerOrbVolley = 6;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Oracle");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.TheEyeOfCthulhu);
            AIType = 554;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.scale = 1.2f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 4;
        }

        public override void AI()
        {
            // Produces golden dust constantly while in flight. This lights the yoyo.
            if(Main.rand.NextBool())
            {
                int dustType = (Main.rand.Next(3) == 0) ? 244 : 246;
                float scale = 0.8f + Main.rand.NextFloat(0.6f);
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType);
                Main.dust[idx].noGravity = true;
                Main.dust[idx].velocity = Vector2.Zero;
                Main.dust[idx].scale = scale;
            }

            // The aura discharges over time based on its current charge.
            float discharge = MinDischargeRate + 0.003f * Projectile.localAI[1];
            if (discharge > MaxDischargeRate)
                discharge = MaxDischargeRate;
            Projectile.localAI[1] -= discharge;

            // Boundary checks on aura charge
            if (Projectile.localAI[1] < 0f)
                Projectile.localAI[1] = 0f;
            if (Projectile.localAI[1] > MaxCharge)
                Projectile.localAI[1] = MaxCharge;

            // If the aura is large enough to be considered "on", draw it, make sound and damage enemies
            if (Projectile.localAI[1] > MinAuraRadius)
            {
                float auraRadius = Projectile.localAI[1] > MaxAuraRadius ? MaxAuraRadius : Projectile.localAI[1];
                DrawRedLightningAura(auraRadius);

                if (Projectile.soundDelay == 0)
                {
                    Projectile.soundDelay = 22;
                    SoundEngine.PlaySound(SoundID.Item93, Projectile.Center);
                }

                // The aura's direct damage scales with its charge: 100 base damage + 1 per charge point
                int auraDamage = (int)(AuraBaseDamage + Projectile.localAI[1]);
                DealAuraDamage(auraRadius, auraDamage);
            }
            else
                Projectile.soundDelay = 2;

            Projectile.netUpdate = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // The yoyo does nothing versus normal dummies
            if (target.type == NPCID.TargetDummy)
                return;
            
            // Charge up the red lightning aura with every hit
            Projectile.localAI[1] += ChargePerHit;

            // Fire Auric orbs every few hits while supercharged.
            if (Projectile.localAI[1] > SuperchargeThreshold && Projectile.numHits % HitsPerOrbVolley == 0)
                FireAuricOrbs();
        }

        // Uses dust type 260, which lives for an extremely short amount of time
        private void DrawRedLightningAura(float radius)
        {
            // Light emits from the yoyo itself while the aura is active, eventually becoming insanely bright
            float brightness = radius * 0.03f;
            Lighting.AddLight(Projectile.Center, brightness, 0.2f * brightness, 0.1f * brightness);

            // Number of particles on the circumference scales directly with the circumference
            float dustDensity = 0.2f;
            int numDust = (int)(dustDensity * MathHelper.TwoPi * radius);
            float angleIncrement = MathHelper.TwoPi / (float)numDust;

            // Incrementally rotate the vector as a ring of dust is drawn
            Vector2 dustOffset = new Vector2(radius, 0f);
            dustOffset = dustOffset.RotatedByRandom(MathHelper.TwoPi);
            for (int i = 0; i < numDust; ++i)
            {
                dustOffset = dustOffset.RotatedBy(angleIncrement);
                int dustType = 260;
                float scale = 1.6f + Main.rand.NextFloat(0.9f);
                int idx = Dust.NewDust(Projectile.Center, 1, 1, dustType);
                Main.dust[idx].position = Projectile.Center + dustOffset;
                Main.dust[idx].noGravity = true;
                Main.dust[idx].noLight = true;
                Main.dust[idx].velocity *= 0.5f;
                Main.dust[idx].scale = scale;
            }

            // Rarely, draw some "arcs" which are lines of dust to the edge
            if (Main.rand.Next(30) == 0)
            {
                int numArcs = 3;
                float arcDustDensity = 0.6f;
                if (Main.rand.NextBool()) ++numArcs;
                if (Main.rand.NextBool()) ++numArcs;

                Vector2 radiusVec = new Vector2(radius, 0f);
                int dustPerArc = (int)(arcDustDensity * radius);
                for (int i = 0; i < numArcs; ++i)
                {
                    radiusVec = radiusVec.RotatedByRandom(MathHelper.TwoPi);
                    for(int j = 0; j < dustPerArc; ++j)
                    {
                        Vector2 partialRadius = ((float)j / dustPerArc) * radiusVec;
                        int dustType = 260;
                        float scale = 1.6f + Main.rand.NextFloat(0.9f);
                        int idx = Dust.NewDust(Projectile.Center, 1, 1, dustType);
                        Main.dust[idx].position = Projectile.Center + partialRadius;
                        Main.dust[idx].noGravity = true;
                        Main.dust[idx].noLight = true;
                        Main.dust[idx].velocity *= 0.3f;
                        Main.dust[idx].scale = scale;
                    }
                }

                // Make extra sound when these arcs happen
                SoundEngine.PlaySound(SoundID.NPCHit53, Projectile.Center);
            }
        }

        private void DealAuraDamage(float radius, int baseDamage)
        {
            if (Projectile.owner != Main.myPlayer)
                return;
            Player owner = Main.player[Projectile.owner];

            for (int i = 0; i < 200; ++i)
            {
                NPC target = Main.npc[i];
                if (!target.active || target.dontTakeDamage || target.friendly)
                    continue;

                // Shock any valid target within range. Check all four corners of their hitbox.
                float d1 = Vector2.Distance(Projectile.Center, target.Hitbox.TopLeft());
                float d2 = Vector2.Distance(Projectile.Center, target.Hitbox.TopRight());
                float d3 = Vector2.Distance(Projectile.Center, target.Hitbox.BottomLeft());
                float d4 = Vector2.Distance(Projectile.Center, target.Hitbox.BottomRight());
                float dist = MathHelper.Min(d1, d2);
                dist = MathHelper.Min(dist, d3);
                dist = MathHelper.Min(dist, d4);
                
                if (dist <= radius)
                {
                    int finalDamage = (int)(baseDamage * owner.GetDamage(DamageClass.Melee).Base * Main.rand.NextFloat(0.85f, 1.15f));
                    bool crit = Main.rand.Next(100) <= owner.GetCritChance(DamageClass.Melee) + 4;
                    target.StrikeNPC(target.CalculateHitInfo(finalDamage, 0, false, 0, DamageClass.Melee, false));

                    if (Main.netMode != 0)
                        NetMessage.SendData(28, -1, -1, null, i, finalDamage, 0f, 0f, crit?1:0, 0, 0);
                }
            }
        }

        private void FireAuricOrbs()
        {
            // Play a sound when orbs are fired
            SoundEngine.PlaySound(SoundID.Item92, Projectile.Center);
            
            int numOrbs = 3;
            int orbID = Mod.Find<ModProjectile>("Orbacle").Type;
            int orbDamage = Oracle.BaseDamage * 3;
            float orbKB = 8f;
            float angleVariance = MathHelper.TwoPi / (float)numOrbs;
            float spinOffsetAngle = MathHelper.Pi / (2f * numOrbs);
            Vector2 posVec = new Vector2(2f, 0f).RotatedByRandom(MathHelper.TwoPi);

            for (int i = 0; i < numOrbs; ++i)
            {
                posVec = posVec.RotatedBy(angleVariance);
                Vector2 velocity = new Vector2(posVec.X, posVec.Y).RotatedBy(spinOffsetAngle);
                velocity.Normalize();
                velocity *= 18f;
                if(Projectile.owner == Main.myPlayer)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.Center + posVec, velocity, orbID, orbDamage, orbKB, Main.myPlayer, 0.0f, 0.0f);
            }
        }
    }
}
