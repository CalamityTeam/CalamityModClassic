using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Plaguebringer
{
	public class PlagueStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plague Staff");
			// Tooltip.SetDefault("Fires a spread of plague fangs");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 78;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 22;
	        Item.width = 46;
	        Item.height = 46;
	        Item.useTime = 21;
	        Item.useAnimation = 21;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 8f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item43;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("PlagueFang").Type;
	        Item.shootSpeed = 16f;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num72 = Item.shootSpeed;
	    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
	    	int num130 = 6;
			if (Main.rand.Next(3) == 0)
			{
				num130++;
			}
			if (Main.rand.Next(4) == 0)
			{
				num130++;
			}
			if (Main.rand.Next(5) == 0)
			{
				num130++;
			}
			for (int num131 = 0; num131 < num130; num131++)
			{
				float num132 = num78;
				float num133 = num79;
				float num134 = 0.05f * (float)num131;
				num132 += (float)Main.rand.Next(-120, 121) * num134;
				num133 += (float)Main.rand.Next(-120, 121) * num134;
				num80 = (float)Math.Sqrt((double)(num132 * num132 + num133 * num133));
				num80 = num72 / num80;
				num132 *= num80;
				num133 *= num80;
				float x2 = vector2.X;
				float y2 = vector2.Y;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), x2, y2, num132, num133, Mod.Find<ModProjectile>("PlagueFang").Type, damage, knockback, Main.myPlayer, 0f, 0f);
			}
			return false;
		}
	}
}