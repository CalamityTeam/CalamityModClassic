using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories
{
    public class HeartoftheElements : ModItem
    {
    	public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 9));
		}
    	
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000000;
            Item.defense = 10;
			Item.accessory = true;
        }
        
        public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
	            }
	        }
	    }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
        	Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, ((float)Main.DiscoR / 255f), ((float)Main.DiscoG / 255f), ((float)Main.DiscoB / 255f));
			player.lifeRegen += 2;
			player.statLifeMax2 += 50;
			player.moveSpeed += 0.1f;
        	player.jumpSpeedBoost += 2.0f;
        	player.endurance += 0.05f;
        	player.statManaMax2 += 50;
			player.manaCost *= 0.95f;
			player.GetCritChance(DamageClass.Melee) += 10;
			player.GetDamage(DamageClass.Melee) += 0.1f;
			player.GetCritChance(DamageClass.Magic) += 10;
			player.GetDamage(DamageClass.Magic) += 0.1f;
			player.GetCritChance(DamageClass.Ranged) += 10;
			player.GetDamage(DamageClass.Ranged) += 0.1f;
			player.GetCritChance(DamageClass.Throwing) += 10;
			player.GetDamage(DamageClass.Throwing) += 0.1f;
			player.GetDamage(DamageClass.Summon) += 0.1f;
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			modPlayer.brimstoneWaifu = true;
			modPlayer.sandWaifu = true;
			modPlayer.sandBoobWaifu = true;
			modPlayer.cloudWaifu = true;
			modPlayer.sirenWaifu = true;
			if (player.whoAmI == Main.myPlayer)
			{
				int damage = NPC.downedMoonlord ? 300 : 100;
				float damageMult = CalamityWorld.downedDoG ? 2.5f : 1f;
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] > 1 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] > 1 ||
				    player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] > 1 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("SandyWaifu").Type] > 1 ||
				    player.ownedProjectileCounts[Mod.Find<ModProjectile>("CloudWaifu").Type] > 1)
				{
					player.ClearBuff(Mod.Find<ModBuff>("BrimstoneWaifu").Type);
					player.ClearBuff(Mod.Find<ModBuff>("SirenLure").Type);
					player.ClearBuff(Mod.Find<ModBuff>("DrewsSandyWaifu").Type);
					player.ClearBuff(Mod.Find<ModBuff>("SandyWaifu").Type);
					player.ClearBuff(Mod.Find<ModBuff>("CloudyWaifu").Type);
				}
				if (player.FindBuffIndex(Mod.Find<ModBuff>("BrimstoneWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("BrimstoneWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("BigBustyRose").Type, (int)((float)damage * damageMult), 2f, Main.myPlayer, 0f, 0f);
				}
				if (player.FindBuffIndex(Mod.Find<ModBuff>("SirenLure").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("SirenLure").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SirenLure").Type, 0, 0f, Main.myPlayer, 0f, 0f);
				}
				if (player.FindBuffIndex(Mod.Find<ModBuff>("DrewsSandyWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("DrewsSandyWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("DrewsSandyWaifu").Type, (int)((float)damage * damageMult * 1.5f), 2f, Main.myPlayer, 0f, 0f);
				}
				if (player.FindBuffIndex(Mod.Find<ModBuff>("SandyWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("SandyWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SandyWaifu").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SandyWaifu").Type, (int)((float)damage * damageMult * 1.5f), 2f, Main.myPlayer, 0f, 0f);
				}
				if (player.FindBuffIndex(Mod.Find<ModBuff>("CloudyWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("CloudyWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("CloudyWaifu").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("CloudyWaifu").Type, (int)((float)damage * damageMult), 2f, Main.myPlayer, 0f, 0f);
				}
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
        
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "WifeinaBottle");
			recipe.AddIngredient(null, "WifeinaBottlewithBoobs");
			recipe.AddIngredient(null, "LureofEnthrallment");
			recipe.AddIngredient(null, "EyeoftheStorm");
			recipe.AddIngredient(null, "RoseStone");
			recipe.AddIngredient(null, "AeroStone");
			recipe.AddIngredient(null, "CryoStone");
			recipe.AddIngredient(null, "ChaosStone");
			recipe.AddIngredient(null, "BloomStone");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
    }
}
