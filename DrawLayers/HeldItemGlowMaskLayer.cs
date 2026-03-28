using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.DrawLayers;

public class HeldItemGlowMaskLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);
	
	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.shadow != 0f)
		{
			return;
		}
		Player drawPlayer = drawInfo.drawPlayer;
		Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
		Item heldItem = drawInfo.heldItem;
		float adjustedItemScale = drawPlayer.GetAdjustedItemScale(heldItem);
		Item item = drawPlayer.inventory[drawPlayer.selectedItem];
		if (!drawPlayer.frozen &&
		    ((drawPlayer.itemAnimation > 0 && item.useStyle != 0) || (item.holdStyle > 0 && !drawPlayer.pulley)) &&
		    item.type > 0 &&
		    !drawPlayer.dead &&
		    !item.noUseGraphic &&
		    (!drawPlayer.wet || !item.noWet))
		{
			Vector2 vector = drawPlayer.position + (drawPlayer.itemLocation - drawPlayer.position);
			SpriteEffects effect = SpriteEffects.FlipHorizontally;
			if (drawPlayer.gravDir == 1f)
			{
				if (drawPlayer.direction == 1)
				{
					effect = SpriteEffects.None;
				}
				else
				{
					effect = SpriteEffects.FlipHorizontally;
				}
			}
			else
			{
				if (drawPlayer.direction == 1)
				{
					effect = SpriteEffects.FlipVertically;
				}
				else
				{
					effect = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
				}
			}
			if (item.type == mod.Find<ModItem>("DeathhailStaff").Type)
			{
				Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Items/Weapons/DevourerofGods/DeathhailStaffGlow").Value;
				float num104 = drawPlayer.itemRotation + 0.785f * (float)drawPlayer.direction;
				int num105 = 0;
				int num106 = 0;
				Vector2 zero3 = new Vector2(0f, (float)TextureAssets.Item[item.type].Value.Height);
				if (drawPlayer.gravDir == -1f)
				{
					if (drawPlayer.direction == -1)
					{
						num104 += 1.57f;
						zero3 = new Vector2((float)TextureAssets.Item[item.type].Value.Width, 0f);
						num105 -= TextureAssets.Item[item.type].Value.Width;
					}
					else
					{
						num104 -= 1.57f;
						zero3 = Vector2.Zero;
					}
				}
				else if (drawPlayer.direction == -1)
				{
					zero3 = new Vector2((float)TextureAssets.Item[item.type].Value.Width, (float)TextureAssets.Item[item.type].Value.Height);
					num105 -= TextureAssets.Item[item.type].Value.Width;
				}
				DrawData data = new DrawData(texture,
					new Vector2((float)((int)(vector.X - Main.screenPosition.X + zero3.X + (float)num105)), (float)((int)(vector.Y - Main.screenPosition.Y + (float)num106))),
					new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.Item[item.type].Value.Width, TextureAssets.Item[item.type].Value.Height)),
					Color.White,
					num104,
					zero3,
					adjustedItemScale,
					effect,
					0);
				drawInfo.DrawDataCache.Add(data);
			}
			else if (item.type == mod.Find<ModItem>("Deathwind").Type)
			{
				Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Items/Weapons/DevourerofGods/DeathwindGlow").Value;
				Vector2 vector13 = new Vector2((float)(TextureAssets.Item[item.type].Value.Width / 2), (float)(TextureAssets.Item[item.type].Value.Height / 2));
				Vector2 vector14 = new Vector2((float)(TextureAssets.Item[item.type].Value.Width / 2), (float)(TextureAssets.Item[item.type].Value.Height / 2));
				int num107 = (int)vector14.X - 10;
				vector13.Y = vector14.Y;
				Vector2 origin4 = new Vector2(-(float)num107, (float)(TextureAssets.Item[item.type].Value.Height / 2));
				if (drawPlayer.direction == -1)
				{
					origin4 = new Vector2((float)(TextureAssets.Item[item.type].Value.Width + num107), (float)(TextureAssets.Item[item.type].Value.Height / 2));
				}
				DrawData data = new DrawData(texture,
					new Vector2((float)((int)(vector.X - Main.screenPosition.X + vector13.X)), (float)((int)(vector.Y - Main.screenPosition.Y + vector13.Y))) - new Vector2((float)texture.Width * 0.5f, 0f),
					new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.Item[item.type].Value.Width, TextureAssets.Item[item.type].Value.Height)),
					Color.White,
					drawPlayer.itemRotation,
					origin4,
					adjustedItemScale,
					effect,
					0);
				drawInfo.DrawDataCache.Add(data);
			}
			else if (item.type == mod.Find<ModItem>("Excelsus").Type)
			{
				Vector2 zero2 = Vector2.Zero;
				Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Items/Weapons/DevourerofGods/ExcelsusGlow").Value;
				if (drawPlayer.gravDir == -1f)
				{
					DrawData data = new DrawData(texture,
						new Vector2((float)((int)(vector.X - Main.screenPosition.X)), (float)((int)(vector.Y - Main.screenPosition.Y))),
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.Item[item.type].Value.Width, TextureAssets.Item[item.type].Value.Height)),
						Color.White,
						drawPlayer.itemRotation,
						new Vector2((float)TextureAssets.Item[item.type].Value.Width * 0.5f - (float)TextureAssets.Item[item.type].Value.Width * 0.5f * (float)drawPlayer.direction, 0f) + zero2,
						adjustedItemScale,
						effect,
						0);
					drawInfo.DrawDataCache.Add(data);
				}
				else
				{
					DrawData data = new DrawData(texture,
						new Vector2((float)((int)(vector.X - Main.screenPosition.X)), (float)((int)(vector.Y - Main.screenPosition.Y))),
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.Item[item.type].Value.Width, TextureAssets.Item[item.type].Value.Height)),
						Color.White,
						drawPlayer.itemRotation,
						new Vector2((float)TextureAssets.Item[item.type].Value.Width * 0.5f - (float)TextureAssets.Item[item.type].Value.Width * 0.5f * (float)drawPlayer.direction, (float)TextureAssets.Item[item.type].Value.Height) + zero2,
						adjustedItemScale,
						effect,
						0);
					drawInfo.DrawDataCache.Add(data);
				}
			}
		}
	}
}