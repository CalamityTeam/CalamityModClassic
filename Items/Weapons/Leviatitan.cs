using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Leviatitan : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/7.Leviathan/Leviatitan");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Leviatitan");
        Item.damage = 72;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 82;
        Item.height = 28;
        ////Tooltip.SetDefault("Fires poisonous sea water and regular sea water blasts\nDoes not require ammo");
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.75f;
        Item.value = 750000;
        Item.rare = 7;
        Item.UseSound = SoundID.Item92;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("AquaBlast").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
        float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
        float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
        if (Main.rand.Next(3) == 0)
        {
        	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),Mod.Find<ModProjectile>("AquaBlastToxic").Type, (int)((double)damage * 1.5f), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        else
        {
        	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), Mod.Find<ModProjectile>("AquaBlast").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        }
    	return false;
	}
}}