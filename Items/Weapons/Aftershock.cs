using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons   //where is located
{
    public class Aftershock : ModItem
    {
    	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
		{
			//texture =("CalamityModClassic1Point1/Items/Weapons/Aftershock");
			return true;
		}
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Aftershock");     //Sword name
            Item.damage = 50;            //Sword damage
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            //if it's melee
            Item.width = 60;              //Sword width
            Item.height = 60;             //Sword height
            ////Tooltip.SetDefault("Summons boulders from the sky on enemy hits");  //Item Description
            Item.useTime = 28;          //how fast 
            Item.useAnimation = 28;  
			Item.useTurn = true;            
            Item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            Item.knockBack = 7.5f;      //Sword knockback
            Item.value = 150000;        
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.shootSpeed = 12f;                //projectile speed                 
        }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 120);  //60 is the buff time
            int i = Main.myPlayer;
			float num72 = Item.shootSpeed;
			int num73 = 50;
			float num74 = Item.knockBack;
	    	num74 = player.GetWeaponKnockback(Item, num74);
	    	player.itemTime = Item.useTime;
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num78 = (float)Main.mouseX - Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY - Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
			}
			float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
			float num81 = num80;
			if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
			{
				num78 = (float)player.direction;
				num79 = 0f;
				num80 = num72;
			}
			else
			{
				num80 = num72 / num80;
			}
	    	num78 *= num80;
			num79 *= num80;
			vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			vector2.Y -= (float)(100);
			num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (num79 < 0f)
			{
				num79 *= -1f;
			}
			if (num79 < 20f)
			{
				num79 = 20f;
			}
			num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
			num80 = num72 / num80;
			num78 *= num80;
			num79 *= num80;
			float speedX4 = num78;
			float speedY5 = num79 + (float)Main.rand.Next(-10, 11) * 0.02f;
			int boulder = Projectile.NewProjectile(player.GetSource_FromThis(), vector2.X, vector2.Y, speedX4, speedY5, ProjectileID.BoulderStaffOfEarth, num73, num74, i, 0f, (float)Main.rand.Next(10));
			Main.projectile[boulder].DamageType = DamageClass.Melee;
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 32);
            }
        }
    }
}