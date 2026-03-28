using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework;
using System;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityModClassicPreTrailer.Items.Mounts
{
	class OnyxExcavator : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 109;
			MountData.spawnDustNoGravity = true;
			MountData.buff = Mod.Find<ModBuff>("OnyxExcavatorBuff").Type;
			MountData.heightBoost = 10;
			MountData.fallDamage = 0f;
			MountData.runSpeed = 8f;
			MountData.flightTimeMax = 0;
			MountData.jumpHeight = 5;
			MountData.acceleration = 0.2f;
			MountData.jumpSpeed = 3f;
			MountData.swimSpeed = 0.5f;
			MountData.totalFrames = 8;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 4;
			}
			array[1] = 2;
			array[5] = 2;
			MountData.playerYOffsets = array;
			MountData.xOffset = 0; //-6
			MountData.bodyFrame = 3;
			MountData.yOffset = 0; //done
			MountData.playerHeadOffset = 10;
			MountData.standingFrameCount = 1;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 8;
			MountData.runningFrameDelay = 36; //36
			MountData.runningFrameStart = MountData.standingFrameStart;
			MountData.inAirFrameCount = MountData.standingFrameCount;
			MountData.inAirFrameDelay = MountData.standingFrameDelay;
			MountData.inAirFrameStart = MountData.standingFrameStart;
			MountData.idleFrameCount = MountData.standingFrameCount;
			MountData.idleFrameDelay = MountData.standingFrameDelay;
			MountData.idleFrameStart = MountData.standingFrameStart;
			MountData.idleFrameLoop = false;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				MountData.backTextureExtra = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Items/Mounts/OnyxExcavatorExtra");
				MountData.frontTextureExtra = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Items/Mounts/OnyxExcavatorExtra2");
				MountData.textureWidth = MountData.backTexture.Value.Width;
				MountData.textureHeight = MountData.backTexture.Value.Height;
			}
		}

		public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
		{
			bool speed = Math.Abs(velocity.X) > mountedPlayer.mount.RunSpeed / 2f;
			float num10 = (float)Math.Sign(mountedPlayer.velocity.X);
			Lighting.AddLight(mountedPlayer.Center, 0.5f, 0.5f, 0.4f);
			if (speed && velocity.Y == 0f)
			{
				for (int i = 0; i < 2; i++)
				{
					Dust expr_631 = Main.dust[Dust.NewDust(mountedPlayer.BottomLeft, mountedPlayer.width, 6, 192, 0f, 0f, 0, default(Color), 1f)];
					expr_631.velocity = new Vector2(velocity.X * 0.15f, Main.rand.NextFloat() * -2f);
					expr_631.noLight = true;
					expr_631.scale = 0.2f + Main.rand.NextFloat() * 0.8f;
					expr_631.fadeIn = 0.5f + Main.rand.NextFloat() * 1f;
					expr_631.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
				}
				if (mountedPlayer.cMount == 0)
				{
					mountedPlayer.position += new Vector2(num10 * 24f, 0f);
					mountedPlayer.FloorVisuals(true);
					mountedPlayer.position -= new Vector2(num10 * 24f, 0f);
				}
			}
			return true;
		}

		public override void UseAbility(Player player, Vector2 mousePosition, bool toggleOn)
		{
			if (Main.mouseLeft && !player.mouseInterface && !Main.blockMouse)
			{
				if (Main.myPlayer == player.whoAmI)
				{
					float num10 = (float)Math.Sign(player.velocity.X);
					if ((double)Math.Abs(num10) < 0.1f)
						num10 = (player.direction == 1 ? 0.1f : -0.1f);
					float num11 = 12f;
					float num12 = 40f;
					Vector2 value2 = player.Center + new Vector2(num10 * num12, num11);
					int num814 = 3;
					int num815 = (int)(value2.X / 16f - (float)num814);
					int num816 = (int)(value2.X / 16f + (float)num814);
					int num817 = (int)(value2.Y / 16f - (float)num814);
					int num818 = (int)(value2.Y / 16f + (float)num814);
					if (num815 < 0)
					{
						num815 = 0;
					}
					if (num816 > Main.maxTilesX)
					{
						num816 = Main.maxTilesX;
					}
					if (num817 < 0)
					{
						num817 = 0;
					}
					if (num818 > Main.maxTilesY)
					{
						num818 = Main.maxTilesY;
					}
					int tileValueLimit = 600 +
						(NPC.downedMechBossAny ? 100 : 0) +
						(NPC.downedPlantBoss ? 100 : 0);
					AchievementsHelper.CurrentlyMining = true;
					for (int num824 = num815; num824 <= num816; num824++)
					{
						for (int num825 = num817; num825 <= num818; num825++)
						{
							value2.Y -= 2f;
							float num826 = Math.Abs((float)num824 - value2.X / 16f);
							float num827 = Math.Abs((float)num825 - value2.Y / 16f);
							double num828 = Math.Sqrt((double)(num826 * num826 + num827 * num827));
							Tile tile = Framing.GetTileSafely(num824, num825);
							if (num828 < (double)num814)
							{
								if (tile != null && tile.HasTile && tile.TileType != (ushort)Mod.Find<ModTile>("AbyssGravel").Type &&
									tile.TileType != (ushort)Mod.Find<ModTile>("Voidstone").Type && (tile.TileType != TileID.Hellstone || Main.hardMode) &&
									(tile.TileType != TileID.LihzahrdBrick || NPC.downedGolemBoss) && tile.TileType != TileID.BlueDungeonBrick &&
									tile.TileType != TileID.GreenDungeonBrick && tile.TileType != TileID.PinkDungeonBrick &&
									(tile.TileType != (ushort)Mod.Find<ModTile>("AstralOre").Type || CalamityWorldPreTrailer.downedStarGod) &&
									((tile.TileType != (ushort)Mod.Find<ModTile>("Tenebris").Type && tile.TileType != (ushort)Mod.Find<ModTile>("PlantyMush").Type) || NPC.downedPlantBoss || CalamityWorldPreTrailer.downedCalamitas) &&
									(!player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea || CalamityWorldPreTrailer.downedDesertScourge) &&
									(Main.tileOreFinderPriority[tile.TileType] < tileValueLimit || tile.TileType == TileID.Heart || tile.TileType == TileID.LifeFruit))
								{
									WorldGen.KillTile(num824, num825, false, false, false);
									if (!Main.tile[num824, num825].HasTile && Main.netMode != 0)
									{
										NetMessage.SendData(17, -1, -1, null, 0, (float)num824, (float)num825, 0f, 0, 0, 0);
									}
								}
							}
						}
					}
					AchievementsHelper.CurrentlyMining = false;
				}
			}
		}
	}
}
