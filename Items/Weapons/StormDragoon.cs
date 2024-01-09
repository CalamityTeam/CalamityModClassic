using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class StormDragoon : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Storm Dragoon");
	}

    public override void SetDefaults()
    {
        Item.damage = 140;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 74;
        Item.height = 34;
        Item.useTime = 2;
        Item.reuseDelay = 10;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.25f;
        Item.value = 1500000;
        Item.UseSound = SoundID.Item31;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 18f;
        Item.useAmmo = 97;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 0);
            }
        }
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-25, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
	    switch (Main.rand.Next(2))
	    {
	    	case 0: type = Mod.Find<ModProjectile>("StormDragoonBlue").Type; break;
	    	case 1: type = Mod.Find<ModProjectile>("StormDragoonPink").Type; break;
	    	default: break;
	    }
	    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 90)
    		return false;
    	return true;
    }
}}