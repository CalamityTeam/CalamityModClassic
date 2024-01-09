using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons.Yharon {
public class DragonRage : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dragon Rage");
	}

	public override void SetDefaults()
	{
		Item.width = 68;  //The width of the .png file in pixels divided by 2.
		Item.damage = 560;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useTime = 25;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 80;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.shoot = Mod.Find<ModProjectile>("DragonRage").Type;
		Item.shootSpeed = 14f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(43, 96, 222);
            }
        }
    }
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
		float SpeedX = velocity.X + 10f * 0.05f;
	    float SpeedY = velocity.Y + 10f * 0.05f;
	    float SpeedX2 = velocity.X - 10f * 0.05f;
	    float SpeedY2 = velocity.Y - 10f * 0.05f;
	    float SpeedX3 = velocity.X + 0f * 0.05f;
	    float SpeedY3 = velocity.Y + 0f * 0.05f;
	    float SpeedX4 = velocity.X - 20f * 0.05f;
	    float SpeedY4 = velocity.Y - 20f * 0.05f;
	    float SpeedX5 = velocity.X + 20f * 0.05f;
	    float SpeedY5 = velocity.Y + 20f * 0.05f;
	    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
	    Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
	    Projectile.NewProjectile(source, position.X, position.Y, SpeedX3, SpeedY3, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
	    Projectile.NewProjectile(source, position.X, position.Y, SpeedX4, SpeedY4, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
	    Projectile.NewProjectile(source, position.X, position.Y, SpeedX5, SpeedY5, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
        return false;
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CopperCoin);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Daybreak, 360);
	}
}}
