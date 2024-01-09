using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.PlaguebringerGoliath
{
    public class BloomStone : ModItem
    {
    	public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 7));
		}
    	
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 500000;
            Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
        	Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.25f, 0.4f, 0.2f);
			player.GetCritChance(DamageClass.Melee) += 2;
			player.GetDamage(DamageClass.Melee) += 0.02f;
			player.GetCritChance(DamageClass.Magic) += 2;
			player.GetDamage(DamageClass.Magic) += 0.02f;
			player.GetCritChance(DamageClass.Ranged) += 2;
			player.GetDamage(DamageClass.Ranged) += 0.02f;
			player.GetCritChance(DamageClass.Throwing) += 2;
			player.GetDamage(DamageClass.Throwing) += 0.02f;
			player.GetDamage(DamageClass.Summon) += 0.02f;
			int bloomCounter = 0;
			int num = 186;
			float num2 = 150f;
			bool flag = bloomCounter % 60 == 0;
			int num3 = 10;
			int random = Main.rand.Next(10);
			if (player.whoAmI == Main.myPlayer)
			{
				if (random == 0)
				{
					for (int l = 0; l < 200; l++)
					{
						NPC nPC = Main.npc[l];
						if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num] && Vector2.Distance(player.Center, nPC.Center) <= num2)
						{
							if (nPC.FindBuffIndex(num) == -1)
							{
								nPC.AddBuff(num, 120, false);
							}
							if (flag)
							{
								nPC.SimpleStrikeNPC(num3, 0, false);
								if (Main.netMode != NetmodeID.SinglePlayer)
								{
									NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, l, (float)num3, 0f, 0f, 0, 0, 0);
								}
							}
						}
					}
				}
			}
			bloomCounter++;
			if (bloomCounter >= 180)
			{
				bloomCounter = 0;
            }
            if (player.whoAmI == Main.myPlayer && player.velocity.Y == 0f && player.grappling[0] == -1)
            {
                int num4 = (int)player.Center.X / 16;
                int num5 = (int)(player.position.Y + (float)player.height - 1f) / 16;
                if (!Main.tile[num4, num5].HasTile && Main.tile[num4, num5].LiquidAmount == 0 && Main.tile[num4, num5 + 1] != null && WorldGen.SolidTile(num4, num5 + 1))
                {
                    Main.tile[num4, num5].TileFrameY = 0;
                    Main.tile[num4, num5].Get<TileWallWireStateData>().Slope = 0;
                    Main.tile[num4, num5].Get<TileWallWireStateData>().IsHalfBlock = false;
                    if (Main.tile[num4, num5 + 1].TileType == 0)
                    {
                        if (Main.rand.NextBool(1000))
                        {

                            if (!Main.tile[num4, num5].HasTile)
                            {
                                WorldGen.PlaceTile(num4, num5, 227);
                            }
                            else
                            {
                                Main.tile[num4, num5].TileType = 227;
                            }
                            Main.tile[num4, num5].TileType = 227;
                            Main.tile[num4, num5].TileFrameX = (short)(34 * Main.rand.Next(1, 13));
                            while (Main.tile[num4, num5].TileFrameX == 144)
                            {
                                Main.tile[num4, num5].TileFrameX = (short)(34 * Main.rand.Next(1, 13));
                            }
                        }
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendTileSquare(-1, num4, num5, 1, TileChangeType.None);
                        }
                    }
                    if (Main.tile[num4, num5 + 1].TileType == 2)
                    {
                        if (Main.rand.NextBool(2))
                        {
                            if (!Main.tile[num4, num5].HasTile)
                            {
                                WorldGen.PlaceTile(num4, num5, 3);
                            }
                            else
                            {
                                Main.tile[num4, num5].TileType = 3;
                            }
                            Main.tile[num4, num5].TileType = 3;
                            Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(6, 11));
                            while (Main.tile[num4, num5].TileFrameX == 144)
                            {
                                Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(6, 11));
                            }
                        }
                        else
                        {
                            if (!Main.tile[num4, num5].HasTile)
                            {
                                WorldGen.PlaceTile(num4, num5, 73);
                            }
                            else
                            {
                                Main.tile[num4, num5].TileType = 73;
                            }
                            Main.tile[num4, num5].TileType = 73;
                            Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(6, 21));
                            while (Main.tile[num4, num5].TileFrameX == 144)
                            {
                                Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(6, 21));
                            }
                        }
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendTileSquare(-1, num4, num5, 1, TileChangeType.None);
                        }
                    }
                    else if (Main.tile[num4, num5 + 1].TileType == 109)
                    {
                        if (Main.rand.NextBool(2))
                        {
                            if (!Main.tile[num4, num5].HasTile)
                            {
                                WorldGen.PlaceTile(num4, num5, 110);
                            }
                            else
                            {
                                Main.tile[num4, num5].TileType = 110;
                            }
                            Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(4, 7));
                            while (Main.tile[num4, num5].TileFrameX == 90)
                            {
                                Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(4, 7));
                            }
                        }
                        else
                        {
                            if (!Main.tile[num4, num5].HasTile)
                            {
                                WorldGen.PlaceTile(num4, num5, 113);
                            }
                            else
                            {
                                Main.tile[num4, num5].TileType = 113;
                            }
                            Main.tile[num4, num5].TileType = 113;
                            Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(2, 8));
                            while (Main.tile[num4, num5].TileFrameX == 90)
                            {
                                Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(2, 8));
                            }
                        }
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendTileSquare(-1, num4, num5, 1, TileChangeType.None);
                        }
                    }
                    else if (Main.tile[num4, num5 + 1].TileType == 60)
                    {
                        if (!Main.tile[num4, num5].HasTile)
                        {
                            WorldGen.PlaceTile(num4, num5, 74);
                        }
                        else
                        {
                            Main.tile[num4, num5].TileType = 74;
                        }
                        Main.tile[num4, num5].TileType = 74;
                        Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(9, 17));
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendTileSquare(-1, num4, num5, 1, TileChangeType.None);
                        }
                    }
                }
            }
        }
    }
}
