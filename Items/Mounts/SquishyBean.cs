using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Mounts
{
    class SquishyBean : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.buff = Mod.Find<ModBuff>("SquishyBeanBuff").Type;
            MountData.heightBoost = 58;
            MountData.fallDamage = -1;
            MountData.runSpeed = 5f;
            MountData.dashSpeed = 8f;
            MountData.fatigueMax = 0;
            MountData.jumpHeight = 20;
            MountData.acceleration = 0.1f;
            MountData.jumpSpeed = 40f; //20
            MountData.blockExtraJumps = true;
            MountData.totalFrames = 4;
            MountData.constantJump = true;
            MountData.usesHover = false;
            int[] array = new int[MountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                switch (l)
                {
                    case 0:
                        array[l] = 44;
                        break;
                    case 1:
                        array[l] = 46;
                        break;
                    case 2:
                        array[l] = 48;
                        break;
                    case 3:
                        array[l] = 48;
                        break;
                }
            }
            MountData.playerYOffsets = array;
            MountData.xOffset = 0;
            MountData.bodyFrame = 3;
            MountData.yOffset = 18;
            MountData.playerHeadOffset = 30;
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 4;
            MountData.runningFrameDelay = 24;
            MountData.runningFrameStart = 0;
            MountData.flyingFrameCount = 0;
            MountData.flyingFrameDelay = 0;
            MountData.flyingFrameStart = 0;
            MountData.inAirFrameCount = 1;
            MountData.inAirFrameDelay = 12;
            MountData.inAirFrameStart = 0;
            MountData.idleFrameCount = 4;
            MountData.idleFrameDelay = 12;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = true;
            MountData.flyingFrameCount = 4;
            MountData.flyingFrameDelay = 12;
            MountData.flyingFrameStart = 0;
            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;
            if (Main.netMode != 2)
            {
                MountData.textureWidth = MountData.backTexture.Value.Width;
                MountData.textureHeight = MountData.backTexture.Value.Height;
            }
        }

        public override void UpdateEffects(Player player)
        {
            player.statDefense += 60;
            player.gravity = 2f;
            player.maxFallSpeed = 25f;
        }
    }
}

